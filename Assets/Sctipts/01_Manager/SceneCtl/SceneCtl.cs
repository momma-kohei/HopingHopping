using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを扱うクラス
/// </summary>
public class SceneCtl : MonoBehaviour
{
    [SerializeField] private EndingCtl _ending;

    private SceneList _nextScene;

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
    public void SetNextScene(SceneList scene)
    {
        _nextScene = scene;
    }

    private void GoToNextScene()
    {
        switch (_nextScene)
        {
            case SceneList.Title:
                LoadScene("Title");
                break;
            case SceneList.Select:
                LoadScene("Select");
                break;
            case SceneList.S1:
                LoadScene("Stage1");
                break;
            case SceneList.S2:
                LoadScene("Stage2");
                break;
            case SceneList.S3:
                LoadScene("Stage3");
                break;
            case SceneList.S4:
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
public enum SceneList
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
