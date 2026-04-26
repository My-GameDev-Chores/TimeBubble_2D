using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TimeBubble : MonoBehaviour
{
    public float duration = 4f;
    public float slowFactor = 0.3f;

    private List<MonoBehaviour> affected = new List<MonoBehaviour>();
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject); // Disappear after duration
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out MovingEnemy moveEnemy))
        {
            moveEnemy.ApplySlow(slowFactor);
            affected.Add(moveEnemy);
        }
        else if (other.TryGetComponent(out FloatingEnemy floatEnemy))
        {
            floatEnemy.ApplySlow(slowFactor);
            affected.Add(floatEnemy);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out MovingEnemy moveEnemy))
        {
            moveEnemy.ResetSlow(slowFactor);
            affected.Remove(moveEnemy);
        }
        else if (other.TryGetComponent(out FloatingEnemy floatEnemy))
        {
            floatEnemy.ResetSlow(slowFactor);
            affected.Remove(floatEnemy);
        }
    }

    void OnDestroy()
    {
        // Reset slowed enemies
        foreach (var enemy in affected)
        {
            if (enemy is MovingEnemy move)
                move.ResetSlow(slowFactor);
            else if (enemy is FloatingEnemy floatE)
                floatE.ResetSlow(slowFactor);
        }

        // Notify Player to allow next bubble
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.ClearBubbleReference();
            }
        }
    }
}
