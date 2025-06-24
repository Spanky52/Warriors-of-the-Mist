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
    private bool playerIsAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();

            if (GameManager.Instance != null && GameManager.Instance.IsPlayerDead)
            {
                playerIsAlive = false;
                animator.SetBool("has_a_target", false);
            }
            else if (playerHealth != null)
            {
                playerHealth.onDeath += OnPlayerDeath;
            }
        }
    }

    void Update()
    {
        if (!playerIsAlive) return;

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

    private void OnPlayerDeath()
    {
        playerIsAlive = false;
        has_a_target = false;
        animator.SetBool("has_a_target", false);
    }
}
