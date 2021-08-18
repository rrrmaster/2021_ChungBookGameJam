using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    [SerializeField]
    private readonly Rigidbody2D rigidbody;

    [SerializeField]
    private readonly Animator animator;

    public Vector3 Velocity
    {
        get { return rigidbody.velocity; }
    }

    public Character(Animator animator, Rigidbody2D rigidbody)
    {
        this.animator = animator;
        this.rigidbody = rigidbody;
    }

    public void Movement(Vector2 velocity)
    {
        var normal = velocity.normalized;

        rigidbody.velocity = velocity;

        animator.SetFloat("x", normal.x);
        animator.SetFloat("y", normal.y);
        animator.SetFloat("velocity", velocity.magnitude);
    }

}
