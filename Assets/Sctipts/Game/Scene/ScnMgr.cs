using UnityEngine;
using System.Collections;

/// <summary>
/// シーン遷移を管理するクラス
/// </summary>
public class ScnMgr : MonoBehaviour
{
    public static ScnMgr Instance { get; private set; } // 静的なインスタンスを保持するプロパティ

    private OPCtl opCtl;
    private EDCtl edCtl;

    private void Awake()
    {
        Instance = this;
        opCtl = GetComponent<OPCtl>();
        edCtl = GetComponent<EDCtl>();
    }

    private void Start()
    {
        opCtl.OPStart(); // OPを開始する
    }

    /// <summary>
    /// enum SceneType の中からシーンを指定して画面遷移する
    /// </summary>
    /// <param name="scene"></param>
    public void TransitScene(SceneType scene)
    {
        Debug.Log("Scene Transition: " + scene.ToString());
        edCtl.EDStart(); // EDを開始する
        StartCoroutine(LoadScene(scene)); // シーンをロードする
    }

    /// <summary>
    /// enum SceneType の中からシーンを指定して遅延を入れてロードする
    /// </summary>
    /// <param name="scene"></param>
    private IEnumerator LoadScene(SceneType scene)
    {
        int sceneIndex = (int)scene;
        yield return new WaitForSeconds(edCtl.EDTime); // EDの時間分待機する
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}

public enum SceneType
{
    Title,
    StageSelect,
    ForestStage,
    DesertStage,
    OceanStage,
    SnowStage,
    GameOver,
}
