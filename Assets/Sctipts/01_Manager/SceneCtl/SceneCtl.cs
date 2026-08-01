using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを扱うクラス
/// </summary>
public class SceneCtl : MonoBehaviour
{
    [SerializeField] private EndingCtl _ending;

    private Scene _nextScene;

    /// <summary>
    /// シーン遷移を行うメソッド
    /// </summary>
    public void TransitScene()
    {
        _ending.EdStart();
        Invoke(nameof(GoToNextScene), _ending.EdTime);
    }
    /// <summary>
    /// 次のシーンをセット
    /// </summary>
    /// <param name="scene">シーン列挙型</param>
    #region// シーン遷移に関するメソッド群
    public void SetNextScene(Scene scene)
    {
        _nextScene = scene;
    }

    private void GoToNextScene()
    {
        switch (_nextScene)
        {
            case Scene.Title:
                LoadScene("Title");
                break;
            case Scene.Select:
                LoadScene("Select");
                break;
            case Scene.S1:
                LoadScene("Stage1");
                break;
            case Scene.S2:
                LoadScene("Stage2");
                break;
            case Scene.S3:
                LoadScene("Stage3");
                break;
            case Scene.S4:
                LoadScene("Stage4");
                break;
        }
    }
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}

/// <summary>
/// シーンの列挙型
/// </summary>
public enum Scene
{
    Title,
    Select,
    S1,
    S2,
    S3,
    S4
}

/// <summary>
/// シーン遷移方式の列挙型
/// </summary>
public enum ScreenType
{
    Fade,
    SlideL,
    SlideR,
    Rotate
}
