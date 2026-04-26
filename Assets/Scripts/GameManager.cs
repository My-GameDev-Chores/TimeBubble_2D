using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalCollectibles = 3;
    private int collected = 0;

    public GameObject winPanel; // Optional: assign a UI panel in Inspector

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Collect()
    {
        collected++;
        Debug.Log("Collected: " + collected + " / " + totalCollectibles);

        if (collected >= totalCollectibles)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        Debug.Log("YOU WIN!");

        // Freeze the game
        Time.timeScale = 0f;

        // Show win panel (if set)
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

}
