using UniRx;
using UnityEngine;
using System.Collections;
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

    private float attackTimeChecker = 0;

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

        if (Input.GetMouseButtonDown(1))
        {
            if (attackTimeChecker >= attackDelay)
            {
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

    public void OnDamage(float damage) // 피해를 받는 기능
    {
        if (!isDead)
        {
            curHealth -= damage;
            if (curHealth <= 0 /*&!dead*/)
            {
                //TODO 사망
                isDead = true;
            }
        }
    }
}
