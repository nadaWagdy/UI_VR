using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonClickManager : MonoBehaviour
{
    public XRRayInteractor rightHandRayInteractor;
    public XRRayInteractor leftHandRayInteractor;
    public ActionBasedController rightHandController;
    public ActionBasedController leftHandController;

    void Update()
    {
        if (rightHandRayInteractor && rightHandController)
        {
            if (IsButtonPressed(rightHandController) && TryGetSceneFromRay(rightHandRayInteractor, out string targetScene))
            {
                UnityEngine.Debug.Log("Right Button pressed!!!");
                LoadScene(targetScene);
            }
        }

        // Check left hand ray interaction
        if (leftHandRayInteractor && leftHandController)
        {
            if (IsButtonPressed(leftHandController) && TryGetSceneFromRay(leftHandRayInteractor, out string targetScene))
            {
                UnityEngine.Debug.Log("Left Button pressed!!!");
                LoadScene(targetScene);
            }
        }
    }

    private bool IsButtonPressed(ActionBasedController controller)
    {
        return controller.selectAction.action.ReadValue<float>() > 0.5f;
    }

    private bool TryGetSceneFromRay(XRRayInteractor rayInteractor, out string targetScene)
    {
        targetScene = null;

        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            UnityEngine.Debug.Log($"Raycast hit: {hit.transform.name}");
            SceneNavigationButton button = hit.transform.GetComponent<SceneNavigationButton>();

            if (button != null)
            {
                UnityEngine.Debug.Log($"Scene button found: {button.targetScene}");
                targetScene = button.targetScene;
                return true;
            }
            else
            {
                UnityEngine.Debug.Log($"No SceneNavigationButton script on object: {hit.transform.name}");
            }
        }
        else
        {
            UnityEngine.Debug.Log("Raycast did not hit any objects.");
        }

        return false;
    }


    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        UnityEngine.Debug.Log("Loading scene: " + sceneName);
    }
}
