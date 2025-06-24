using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;
    private float lastHitTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastHitTime + damageCooldown)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            Move playerMove = collision.gameObject.GetComponent<Move>();

            if (playerHealth != null && playerMove != null)
            {
                playerHealth.TakeDamage(damage);
                playerMove.TakeDamage(transform.position);
                lastHitTime = Time.time;
            }
        }
    }
}
