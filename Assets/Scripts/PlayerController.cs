using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float minJumpHeight = 2f;
    public float maxJumpHeight = 5f;
    public float maxHoldTime = 3f;
    public float jumpSpeed = 10f;

    [Header("Time Bubble Settings")]
    public GameObject timeBubblePrefab;
    private GameObject currentBubble;
    private InputSystem_Actions inputActions;
    private Vector2 moveInput = Vector2.zero;

    private bool isJumpHeld = false;
    private bool isJumping = false;
    private float jumpHoldTime = 0f;
    private float targetJumpHeight = 0f;
    private float currentJumpHeight = 0f;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();

        // Movement from On-Screen Joystick
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Jump is handled by UI button via JumpChargeButton.cs
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        Vector2 movement = new Vector2(moveInput.x * moveSpeed * Time.deltaTime, 0f);
        transform.Translate(movement);
    }

    private void HandleJump()
    {
        // Charge jump while button is held
        if (isJumpHeld)
        {
            jumpHoldTime += Time.deltaTime;
            jumpHoldTime = Mathf.Min(jumpHoldTime, maxHoldTime);
        }

        // Perform the jump
        if (isJumping)
        {
            float step = jumpSpeed * Time.deltaTime;
            float remaining = targetJumpHeight - currentJumpHeight;

            float moveUp = Mathf.Min(step, remaining);
            transform.Translate(Vector2.up * moveUp);
            currentJumpHeight += moveUp;

            if (currentJumpHeight >= targetJumpHeight)
            {
                isJumping = false;
            }
        }
    }

    // Called by Jump Button (PointerDown)
    public void StartJumpCharge()
    {
        if (!isJumping)
        {
            isJumpHeld = true;
            jumpHoldTime = 0f;
        }
    }

    // Called by Jump Button (PointerUp)
    public void ReleaseJumpCharge()
    {
        if (isJumpHeld && !isJumping)
        {
            isJumpHeld = false;
            currentJumpHeight = 0f;

            float chargeRatio = Mathf.Clamp01(jumpHoldTime / maxHoldTime);
            targetJumpHeight = Mathf.Lerp(minJumpHeight, maxJumpHeight, chargeRatio);
            isJumping = true;
        }
    }

    // Called by Time Bubble UI Button


    public void ActivateTimeBubble()
    {
        // If no bubble exists, spawn one
        if (currentBubble == null && timeBubblePrefab != null)
        {
            currentBubble = Instantiate(timeBubblePrefab, transform.position, Quaternion.identity);
        }
    }

    public void ClearBubbleReference()
    {
        currentBubble = null;
    }

}
