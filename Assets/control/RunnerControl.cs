using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RunnerControl : MonoBehaviour
{
    public float speed = 0f;

    public float jumpDamping = 0f;
    public float jumpPower = 0f;

    private new Rigidbody2D rigidbody2D;
    private Vector2 velocity;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        velocity.x = speed;

        rigidbody2D.AddForce(velocity - rigidbody2D.velocity, ForceMode2D.Force);
        velocity.y = Mathf.Lerp(velocity.y, 0, Time.fixedDeltaTime * jumpDamping);
        // velocity.y = 0;
    }

    public void Jump()
    {
        velocity.y = jumpPower;
    }
}
