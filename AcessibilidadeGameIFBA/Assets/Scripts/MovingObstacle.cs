using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Requirements")]
    public Rigidbody2D rb;

    private float moveSpeed = 5f;

    void Start()
    {
        Destroy(gameObject, 15f);
    }

    void Update()
    {
        rb.linearVelocity = Vector2.left * moveSpeed;
    }
}
