using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 move;
    public Transform player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool has_a_target = false;

    public float range_agro = 15f;
    public float speed = 5f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        Follow_Player();

    }


    public void Follow_Player()
    {
        float distance_player = Vector2.Distance(transform.position, player.position);

        if (distance_player < range_agro)
        {
            has_a_target = true;

            Vector2 direction = (player.position - transform.position).normalized;
            move = new Vector2(direction.x, direction.y);

            if (player.position.x < transform.position.x) { spriteRenderer.flipX = true; }
            if (player.position.x > transform.position.x) { spriteRenderer.flipX = false; }
        }

        else
        {
            has_a_target = false;
            move = Vector2.zero;
        }

        animator.SetBool("has_a_target", has_a_target);
        rb.MovePosition(rb.position + move * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Take_Damage() de Player
        }
    }
}