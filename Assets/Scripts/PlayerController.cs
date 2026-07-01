using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce = 5f;
    [SerializeField] private float maxSpeed = 15f;
    private Rigidbody playerRigidbody;
    private Vector2 moveInput;
    private InputSystem_Actions playerControls;
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);
        // Prevent faster diagonal movement
        if (moveVector.sqrMagnitude > 1f)
        {
            moveVector.Normalize();
        }
        playerRigidbody.AddForce(moveVector * moveForce, ForceMode.Force);
        // Limit the player's speed to maxSpeed
        if (playerRigidbody.linearVelocity.magnitude > maxSpeed)
        {
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity.normalized * maxSpeed;
        }
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
    private void OnDestroy()
    {
        playerControls.Dispose();
    }
}
