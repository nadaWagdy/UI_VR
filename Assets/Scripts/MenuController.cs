using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadToolScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        UnityEngine.Debug.Log("here!");
    }
}