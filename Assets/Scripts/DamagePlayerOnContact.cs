using UnityEngine;

using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f; // tiempo entre golpes
    private float lastHitTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastHitTime + damageCooldown)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastHitTime = Time.time;
            }
        }
    }
}
