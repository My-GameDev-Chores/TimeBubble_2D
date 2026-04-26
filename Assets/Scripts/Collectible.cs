using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected by player!");
            GameManager.Instance.Collect();
            Destroy(gameObject);
        }
    }
}
