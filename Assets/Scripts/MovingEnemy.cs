using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingEnemy : MonoBehaviour
{
    public float moveDistance = 3f;
    public float baseSpeed = 2f;
    private float currentSpeed;
    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        float move = currentSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * move);
            if (transform.position.x >= startPos.x + moveDistance)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * move);
            if (transform.position.x <= startPos.x - moveDistance)
                movingRight = true;
        }
    }

    public void ApplySlow(float slowFactor)
    {
        currentSpeed = baseSpeed * slowFactor;
    }

    public void ResetSlow(float slowFactor)
    {
        currentSpeed = baseSpeed;
    }

    // 👇 Add this method for detecting the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by MovingEnemy!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
