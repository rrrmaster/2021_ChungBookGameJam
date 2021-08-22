using UniRx;
using UnityEngine;
using System.Collections;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public LayerMask whatIsEnemy;

    public float speed = 10;
    private float originSpeed;

    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackPower;

    private bool isDead = false;
    private bool isInvincible = false;
    [Inject]
    public GameModel gameModel;

    [Inject]
    private GamePresenter gamePresenter;

    private float attackTimeChecker = 0;
    private float washingTimeChecker = 0;

    private void Start()
    {
        curHealth = maxHealth;
        originSpeed = speed;
        isDead = false;

        Observable.EveryUpdate()
            .Select(_ => new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0))
            .Subscribe(v => Move(v));

        Observable.EveryLateUpdate().Subscribe(_ => Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10));

    }

    private void Update()
    {
        attackTimeChecker += Time.deltaTime;
        washingTimeChecker += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !gameModel.IsUseItem.Value)
        {
            StartCoroutine(SetSpeedZero(1.5f));
            if (washingTimeChecker >= 1.5f)
            {
                SoundManager.Instance.PlayFXSound("Water");
                animator.SetTrigger("Washing");
                washingTimeChecker = 0;
                var playerPos = transform.position;
                var key = new Vector2Int(Mathf.FloorToInt(playerPos.x), Mathf.FloorToInt(playerPos.y - 0.5f));
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        var nKey = new Vector2Int(x, y) + key;
                        if (gamePresenter.map.ContainsKey(nKey))
                        {
                            gamePresenter.map[nKey].GetComponent<Crop>().IsTodayWashing = true;
                            gamePresenter.Washing(nKey);
                        }

                    }

                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (attackTimeChecker >= attackDelay)
            {
                SoundManager.Instance.PlayFXSound("Attack");
                //TODO 어택
                animator.SetTrigger("Attack");
                attackTimeChecker = 0;
                StartCoroutine(SetSpeedZero(0.2f));
                Attack();
            }
        }
    }

    private void Attack()
    {
        Vector3 point = transform.position;
        point.x += transform.GetChild(0).transform.localScale.x * -1.5f;

        Collider2D[] colls = Physics2D.OverlapCircleAll(point, attackRange, whatIsEnemy);

        //충돌 발생시
        foreach (var item in colls)
        {
            print(item.name);
            Monster monster;
            item.gameObject.TryGetComponent<Monster>(out monster);

            if (monster != null)
            {
                monster.OnDamage(attackPower);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Vector3 point = transform.position;
        point.x += transform.GetChild(0).transform.localScale.x * -1.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point, attackRange);
    }

    private IEnumerator SetSpeedZero(float delay)
    {
        speed = 0;
        yield return new WaitForSeconds(delay);
        speed = originSpeed;
    }

    private void Move(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0)
        {
            transform.GetChild(0).transform.localScale = new Vector3(-Mathf.Sign(velocity.x), 1);
        }

        animator.SetFloat("x", velocity.x);
        animator.SetFloat("y", velocity.y);
        animator.SetFloat("velocity", velocity.magnitude);

        rigidbody.velocity = velocity.normalized * speed;
    }

    private IEnumerator Invincible(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    public void SetPlayerInvincible()
    {
        StartCoroutine(Invincible(3f));
    }

    public void OnDamage(float damage) // 피해를 받는 기능
    {
        if (isInvincible) return;

        gameModel.Health.Value -= 1;
        if (gameModel.Health.Value == 0)
        {
            FindObjectOfType<DungeonManager>().QuitDungeon();
            gamePresenter.NextDay();
            LoseGold();
        }
    }

    private void LoseGold()
    {
        gameModel.Gold.Value = gameModel.Gold.Value - (int)((float)gameModel.Gold.Value * 0.1f);
    }
}
