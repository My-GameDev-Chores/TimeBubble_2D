using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingEnemy : MonoBehaviour
{
    public float baseFloatHeight = 1f;
    public float baseFloatSpeed = 2f;

    private float currentSpeed;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        currentSpeed = baseFloatSpeed;
    }

    void Update()
    {
        float floatY = Mathf.Sin(Time.time * currentSpeed) * baseFloatHeight;
        transform.position = new Vector3(transform.position.x, startPos.y + floatY, transform.position.z);
    }

    public void ApplySlow(float slowFactor)
    {
        currentSpeed = baseFloatSpeed * slowFactor;
    }

    public void ResetSlow(float slowFactor)
    {
        currentSpeed = baseFloatSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by FloatingEnemy!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
