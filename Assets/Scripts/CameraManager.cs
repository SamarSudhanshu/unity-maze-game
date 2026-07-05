using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera followCamera;
    [SerializeField] private Camera topDownCamera;

    [SerializeField] private bool isFollowCameraActive = true;

    private InputSystem_Actions playerControls;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
        playerControls.Player.SwitchCamera.performed += OnSwitchCamera;
    }

    private void Start()
    {
        UpdateCameraState();
    }

    private void OnSwitchCamera(InputAction.CallbackContext context)
    {
        ToggleCamera();
    }

    public void ToggleCamera()
    {
        isFollowCameraActive = !isFollowCameraActive;
        UpdateCameraState();
    }

    private void UpdateCameraState()
    {
        followCamera.gameObject.SetActive(isFollowCameraActive);
        topDownCamera.gameObject.SetActive(!isFollowCameraActive);
    }

    private void OnDisable()
    {
        playerControls.Player.SwitchCamera.performed -= OnSwitchCamera;
        playerControls.Player.Disable();
    }

    private void OnDestroy()
    {
        playerControls.Dispose();
    }
}