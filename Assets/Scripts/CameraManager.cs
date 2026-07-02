using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera followCamera;
    [SerializeField] private Camera topDownCamera;
    [SerializeField] private bool isFollowCameraActive = true;
    private InputSystem_Actions playerControls;
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        playerControls = new InputSystem_Actions();
    }
    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        playerControls.Player.Enable();
        playerControls.Player.SwitchCamera.performed += OnSwitchCamera;
    }
    // Start is called before the first frame update
    private void Start()
    {
        followCamera.gameObject.SetActive(isFollowCameraActive);
        topDownCamera.gameObject.SetActive(!isFollowCameraActive);
    }
    // OnSwitchCamera is called when the SwitchCamera input action is performed
    private void OnSwitchCamera(InputAction.CallbackContext context)
    {
        isFollowCameraActive = !isFollowCameraActive;

        followCamera.gameObject.SetActive(isFollowCameraActive);
        topDownCamera.gameObject.SetActive(!isFollowCameraActive);
    }
    // OnDisable is called when the behaviour becomes disabled or inactive
    private void OnDisable()
    {
        playerControls.Player.SwitchCamera.performed -= OnSwitchCamera;
        playerControls.Player.Disable();
    }
    // OnDestroy is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        playerControls.Dispose();
    }
}
