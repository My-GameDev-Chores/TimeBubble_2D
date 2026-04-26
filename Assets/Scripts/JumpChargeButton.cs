using UnityEngine;
using UnityEngine.EventSystems;

public class JumpChargeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
            player.StartJumpCharge();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (player != null)
            player.ReleaseJumpCharge();
    }
}
