using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        // Optional: Add a short delay or effect here
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
