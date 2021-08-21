using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMonsterState
{
    Idle,
    Chase,
    Attack,
    Dead
}

[RequireComponent(typeof(SpriteRenderer))]
public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterScriptableObject monsterScriptableObject;

    private string fullName;
    private float maxHealth;
    private float attackPower;
    private float attackSpeed;
    private float attackRange;
    private float chaseRange;
    private float moveSpeed;

    private RuntimeAnimatorController animatorController;
    private Animator animator;

    [SerializeField] private float curHealth; //현제 체력
    [SerializeField] private EMonsterState monsterState = new EMonsterState(); //현제 상태\
    private SpriteRenderer spriteRenderer;

    private Transform target;
    private Vector3 randDir = Vector3.zero;

    private float dirChangeTimeChecker = 0;
    private float attackTimeChecker = 0;

    private bool flipOrigin;

    private void Start()
    {
        Substantialization();
        Initialization();
    }

    private void Update()
    {
        attackTimeChecker += Time.deltaTime;
        dirChangeTimeChecker += Time.deltaTime;
        switch (monsterState)
        {
            case EMonsterState.Idle:
                OnIdleState();
                CheckStage();
                break;
            case EMonsterState.Chase:
                OnChaseState();
                CheckStage();
                break;
            case EMonsterState.Attack:
                OnAttackState();
                CheckStage();
                break;
            case EMonsterState.Dead:
                OnDeadState();
                break;
        }
    }

    private void OnIdleState()
    {
        if (dirChangeTimeChecker > 5f)
        {
            SetRandomDirection();
            dirChangeTimeChecker = 0;
        }

        MoveInRandomDirection();
    }

    private void OnChaseState()
    {
        MoveToTarget();
    }

    private void OnAttackState()
    {
        if (attackTimeChecker >= attackSpeed)
        {
            //TODO 어택
            animator.SetTrigger("Attack");
            attackTimeChecker = 0;
        }
    }

    private void OnDeadState()
    {

    }

    private void Substantialization()
    {
        fullName = monsterScriptableObject.FullName;
        maxHealth = monsterScriptableObject.MaxHealth;
        attackPower = monsterScriptableObject.AttackPower;
        attackSpeed = monsterScriptableObject.AttackSpeed;
        attackRange = monsterScriptableObject.AttackRange;
        chaseRange = monsterScriptableObject.ChaseRange;
        moveSpeed = monsterScriptableObject.MoveSpeed;

        animatorController = monsterScriptableObject.AnimatorController;
    }

    private void Initialization()
    {
        curHealth = maxHealth;
        monsterState = EMonsterState.Idle;
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;

        flipOrigin = spriteRenderer.flipX;

        SetRandomDirection();
    }

    private void SetRandomDirection()
    {
        randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    private void MoveInRandomDirection()
    {
        FlipSprite(randDir + transform.position);
        transform.Translate(randDir * (moveSpeed * Time.deltaTime)); // 이동

        //나중에 이동범위 제한 해야함
        // float x = Mathf.Clamp(transform.position.x, minXY.x, maxXY.x); 
        // float y = Mathf.Clamp(transform.position.y, minXY.y, maxXY.y);
        //transform.position = new Vector3(x, y, 0);
    }

    private void MoveToTarget()
    {
        FlipSprite(target.position);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void CheckStage()
    {
        if ((transform.position - target.position).sqrMagnitude < attackRange * attackRange)
        {
            animator.SetBool("IsWalk", false);
            monsterState = EMonsterState.Attack;
        }
        else if ((transform.position - target.position).sqrMagnitude < chaseRange * chaseRange)
        {
            animator.SetBool("IsWalk", true);
            monsterState = EMonsterState.Chase;
        }
        else
        {
            monsterState = EMonsterState.Idle;
        }
    }


    public void OnDamage(float damage) // 피해를 받는 기능
    {
        if (!(monsterState == EMonsterState.Dead))
        {
            curHealth -= damage;
            if (curHealth <= 0 /*&!dead*/)
            {
                monsterState = EMonsterState.Dead;
                //TODO 사망
            }
        }
    }

    private void FlipSprite(Vector3 target)
    {
        if (transform.position.x > target.x) spriteRenderer.flipX = flipOrigin;
        else spriteRenderer.flipX = !flipOrigin;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
