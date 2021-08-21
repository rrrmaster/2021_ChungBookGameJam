using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Monster")]
public class MonsterScriptableObject : ScriptableObject
{
    [Header("이름")] [SerializeField] private string fullName;
    [Header("최대체력")] [SerializeField] private float maxHealth;
    [Header("공격력")] [SerializeField] private float attackPower;
    [Header("공격속도")] [SerializeField] private float attackDelay;
    [Header("공격범위")] [SerializeField] private float attackRange;
    [Header("추격범위")] [SerializeField] private float chaseRange;
    [Header("이동속도")] [SerializeField] private float moveSpeed;

    [Header("애니메이터")] [SerializeField] private RuntimeAnimatorController animatorController;

    public string FullName => fullName;
    public float MaxHealth => maxHealth;
    public float AttackPower => attackPower;
    public float AttackDelay => attackDelay;
    public float AttackRange => attackRange;
    public float ChaseRange => chaseRange;
    public float MoveSpeed => moveSpeed;

    public RuntimeAnimatorController AnimatorController => animatorController;


}
