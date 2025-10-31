using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [Header("フェードスクリプト")] public FadeUp fadeUp;

    private bool firstPush = false;
    private bool goneNextScene = false;

    // called when the botton pressed
    public void PressStart()
    {
        Debug.Log("Pressed!!"); // button の動作確認
        if (!firstPush) {
            Debug.Log("Go Next Scene");
            fadeUp.StartFadeOut();
            firstPush = true;
        }
    }

    private void Update()
    {
        if (!goneNextScene && fadeUp.IsFadeOutFinished())
        {
            SceneManager.LoadScene("stage1");
            goneNextScene = true;
        }
    }
}
