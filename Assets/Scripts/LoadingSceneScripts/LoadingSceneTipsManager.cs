using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneTipsManager : MonoBehaviour
{
    public string[] tips = new string[5] {
        "Tip 1 .... This is the first tip about the tool",
        "Tip 2 .... This is the second tip about the tool",
        "Tip 3 .... This is the third tip about the tool",
        "Tip 4 .... This is the forth tip about the tool",
        "Tip 5 .... This is the fifth tip about the tool"
    };

    public TMP_Text tipsText;

    public float tipChangeInterval = 4f;

    private int currentTipIndex = 0;
    private float timer;

    public string sceneToLoad;

    void Start()
    {
        if (tips.Length == 0 || tipsText == null)
        {
            UnityEngine.Debug.LogError("Tips or TipsText is not configured properly.");
            return;
        }

        tipsText.text = tips[currentTipIndex];
        timer = tipChangeInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ShowNextTip();
            timer = tipChangeInterval;
        }
    }

    private void ShowNextTip()
    {
        currentTipIndex++;

        if (currentTipIndex >= tips.Length)
        {
            LoadNextScene();
        }
        else
        {
            tipsText.text = tips[currentTipIndex];
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
