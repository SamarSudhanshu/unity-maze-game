using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveForce = 5f;
    [SerializeField] private float maxSpeed = 15f;

    [Header("Mobile")]
    [SerializeField] private bool useAccelerometer = true;
    [SerializeField] private float accelerometerSensitivity = 2f;
    [SerializeField] private float deadZone = 0.05f;

    private Rigidbody playerRigidbody;
    private Vector2 moveInput;
    private InputSystem_Actions playerControls;

    public float AccelerometerSensitivity
    {
        get => accelerometerSensitivity;
        set => accelerometerSensitivity = value;
    }

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        playerRigidbody = GetComponent<Rigidbody>();

        accelerometerSensitivity = PlayerPrefs.GetFloat(
            "Sensitivity",
            accelerometerSensitivity);
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();

        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }

    private void Update()
    {
        if (useAccelerometer &&
            Application.platform == RuntimePlatform.Android &&
            Accelerometer.current != null)
        {
            Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();

            Vector2 input = new Vector2(acceleration.x, acceleration.y);

            if (input.magnitude < deadZone)
            {
                input = Vector2.zero;
            }

            moveInput = input * accelerometerSensitivity;
        }
        else
        {
            moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(moveInput.x, 0f, moveInput.y);

        if (moveVector.sqrMagnitude > 1f)
        {
            moveVector.Normalize();
        }

        playerRigidbody.AddForce(moveVector * moveForce, ForceMode.Force);

        if (playerRigidbody.linearVelocity.magnitude > maxSpeed)
        {
            playerRigidbody.linearVelocity =
                playerRigidbody.linearVelocity.normalized * maxSpeed;
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