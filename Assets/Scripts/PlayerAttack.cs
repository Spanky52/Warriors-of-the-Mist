using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Move moveScript;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveScript = GetComponent<Move>();
    }

    public void TriggerAttack()
    {
        if (isAttacking || animator.GetBool("isDead")) return;

        isAttacking = true;
        animator.SetTrigger("Atack_Katana_Player");

        // Bloquea el movimiento desde Move.cs
        if (moveScript != null)
        {
            moveScript.BlockMovement();
        }
    }

    // Este se llama desde Animation Event
    public void ApplyAttackDamage()
    {
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        Vector2 origin = (Vector2)transform.position + direction * 0.5f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(origin, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }

        Debug.Log("Daño aplicado durante la animación");
    }

    // Este se llama al final de la animación
    public void FinishAttack()
    {
        isAttacking = false;

        if (moveScript != null)
        {
            moveScript.UnblockMovement();
        }
    }

    public void CancelAttack()
    {
        isAttacking = false;
        animator.ResetTrigger("Atack_Katana_Player");

        if (moveScript != null)
        {
            moveScript.UnblockMovement();
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
