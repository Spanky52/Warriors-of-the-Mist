using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // estamos usando la tecla espacio por ahora 
        {
            Attack();
        }
    }

    void Attack()
    {
        // detecta enemigos en un circulo
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }

        Debug.Log("Ejecutando ataque");
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiza el rango de ataque en la escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}