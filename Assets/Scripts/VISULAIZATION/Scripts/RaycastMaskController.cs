using UnityEngine;

using UnityEngine.InputSystem;  // For the InputAction
public class RaycastMaskController : MonoBehaviour
{

    public UnityEngine.XR.Interaction.Toolkit.XRRayInteractor leftRayInteractor;
    public UnityEngine.XR.Interaction.Toolkit.XRRayInteractor rightRayInteractor;
    public LayerMask defaultMask; // Store the default raycastMask
    public LayerMask alternateMask; // Store the mask to apply when the button is pressed

    public InputActionProperty leftControllerSecondaryButton; // Input Action for the left secondary button
    public InputActionProperty rightControllerSecondaryButton; // Input Action for the right secondary button

    private void Start()
    {
        Debug.Log("Default mask is set");
        leftRayInteractor.raycastMask = defaultMask;
        rightRayInteractor.raycastMask = defaultMask;
    }

    private void OnEnable()
    {
        Debug.Log("Object is enabled");
        // Subscribe to input action events
        leftControllerSecondaryButton.action.performed += OnSecondaryButtonPressed;
        leftControllerSecondaryButton.action.canceled += OnSecondaryButtonReleased;
        rightControllerSecondaryButton.action.performed += OnSecondaryButtonPressed;
        rightControllerSecondaryButton.action.canceled += OnSecondaryButtonReleased;
    }

    private void OnDisable()
    {
        Debug.Log("Object is Disabled");
        // Unsubscribe from input action events
        leftControllerSecondaryButton.action.performed -= OnSecondaryButtonPressed;
        leftControllerSecondaryButton.action.canceled -= OnSecondaryButtonReleased;
        rightControllerSecondaryButton.action.performed -= OnSecondaryButtonPressed;
        rightControllerSecondaryButton.action.canceled -= OnSecondaryButtonReleased;
    }

    private void OnSecondaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("second button pressed");
        // Change the raycastMask to the alternate mask for both controllers
        leftRayInteractor.raycastMask = alternateMask;
        rightRayInteractor.raycastMask = alternateMask;
    }

    private void OnSecondaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("second button released");
        // Change the raycastMask back to the default mask for both controllers
        leftRayInteractor.raycastMask = defaultMask;
        rightRayInteractor.raycastMask = defaultMask;
    }
}
