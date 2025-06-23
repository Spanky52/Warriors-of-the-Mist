using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public Direction direction;                                 //direction of the stairs
    [Space]
    public string layerUpper;
    public string sortingLayerUpper;
    [Space]
    public string layerLower;
    public string sortingLayerLower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger Enter: {other.gameObject.name} ({other.GetType()})");
        if (direction == Direction.South && other.transform.position.y < transform.position.y) SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);
        else
        if (direction == Direction.West && other.transform.position.x < transform.position.x) SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);
        else
        if (direction == Direction.East && other.transform.position.x > transform.position.x) SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Trigger Enter: {other.gameObject.name} ({other.GetType()})");   
        if (direction == Direction.South && other.transform.position.y < transform.position.y) SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);
        else
        if (direction == Direction.West && other.transform.position.x < transform.position.x) SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);
        else
        if (direction == Direction.East && other.transform.position.x > transform.position.x) SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);
    }

    private void SetLayerAndSortingLayer(GameObject target, string layer, string sortingLayer)
    {
        target.layer = LayerMask.NameToLayer(layer);

        target.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
        SpriteRenderer[] srs = target.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }
    }

}
public enum Direction
{
    North,
    South,
    West,
    East
}