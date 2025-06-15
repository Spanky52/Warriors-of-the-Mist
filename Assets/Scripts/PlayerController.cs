using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Joystick movementJoystick;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public bool take_damage;
    public float speed = 5f;
    public float deffub_knock = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // pa voltear el sprite
    }

    private void FixedUpdate()
    {
        Movement();
    }


    public void Movement()
    {
        Vector2 direction = movementJoystick.Direction;

        if (direction.magnitude > 0.1f)
        {
            rb.linearVelocity = direction * speed;

            // voltear sprite
            if (direction.x != 0)
            {
                spriteRenderer.flipX = direction.x < 0;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // animacion
        animator.SetFloat("Speed", direction.magnitude);
    }

    public void Take_Damage()
    {

        animator.SetBool("take_damage", take_damage);

        R_Damage();

    }

    public void R_Damage()
    {
        take_damage = false;

        Vector2 knock_back = new Vector2(transform.position.x, 0).normalized;
        rb.AddForce(knock_back * deffub_knock, ForceMode2D.Impulse);
    }
}

