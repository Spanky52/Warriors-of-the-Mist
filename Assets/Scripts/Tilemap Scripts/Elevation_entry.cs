using UnityEngine;

public class EnterLayer : MonoBehaviour
{
    public Collider2D[] mountainColliders;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with: " + other.name);
        if (other.CompareTag("Player"))
        {
            foreach (Collider2D mountainCollider in mountainColliders)
            {
                mountainCollider.enabled = false; // Enable the mountain collider
            }
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15; // Set the player state to on mountain
            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Grass_Hight"; // Set the player sorting layer to on mountain
        }
    }
}
