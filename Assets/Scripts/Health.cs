using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    public delegate void OnDeath();
    public event OnDeath onDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibió {damage} de daño. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} murió.");
        onDeath?.Invoke();

        if (CompareTag("Player"))
        {
            Move move = GetComponent<Move>();
            if (move != null)
            {
                move.OnDeath();
            }

            // Notificar al GameManager
            GameManager.Instance?.SetPlayerDead();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
