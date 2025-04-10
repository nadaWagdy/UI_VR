using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClickManager : MonoBehaviour
{
    public ActionBasedController rightHandController;
    public ActionBasedController leftHandController;

    private string sceneToLoad = "ToolScene";

    void Update()
    {
        if (rightHandController)
        {
            if (rightHandController.selectAction.action.ReadValue<float>() > 0.5f)
            {
                OnRightHandButtonPressed();
            }
        }

        if (leftHandController)
        {
            if (leftHandController.selectAction.action.ReadValue<float>() > 0.5f)
            {
                OnLeftHandButtonPressed();
            }
        }
    }

    public void OnRightHandButtonPressed()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
            UnityEngine.Debug.Log("in the right function");
        }
    }

    public void OnLeftHandButtonPressed()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
            UnityEngine.Debug.Log("in the left function");
        }
    }

    public void SetSceneToLoad(string sceneName)
    {
        sceneToLoad = sceneName;
        UnityEngine.Debug.Log("in the load scene function");
    }
}
