using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Requirements")]
    public Rigidbody2D rb;
    public Animator anim;

    private float jumpForce = 6f;
    private bool onGround;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            anim.SetBool("OnGround", true);
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            anim.SetBool("OnGround", false);
            onGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Killzone"))
        {
            GameManager.Instance.MinigameOver();
        }
    }

    private void Jump()
    {
        if (onGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
