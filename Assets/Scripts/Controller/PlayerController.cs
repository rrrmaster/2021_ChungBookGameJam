using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;

    public float speed = 10;

    private void Start()
    {
        Observable.EveryUpdate()
            .Select(_ => new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0))
            .Subscribe(v => Move(v));
        
        Observable.EveryLateUpdate().Subscribe(_ => Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10));
    }

    private void Move(Vector2 velocity)
    {
        animator.SetFloat("x", velocity.x);
        animator.SetFloat("y", velocity.y);
        animator.SetFloat("velocity", velocity.magnitude);

        rigidbody.velocity = velocity.normalized * speed;
    }
}
