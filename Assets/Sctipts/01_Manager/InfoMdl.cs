using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム共通の変数を扱うクラス
/// </summary>
public class InfoMdl : MonoBehaviour
{
    [Header("共通変数")]
    [SerializeField] private int latestStageIndex = 0;
    [SerializeField] private int nextStageIndex = 0;
    [SerializeField] private int currentLife = 3;
    [SerializeField] private string currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    #region// 共通変数へのアクセス
    public int LatestStageIndex => latestStageIndex;
    public int NextStageIndex { get { return nextStageIndex; } set { nextStageIndex = value; } }
    public int CurrentLife => currentLife;
    public string CurrentScene => currentScene;
    public void AddLatestStage()
    {
        latestStageIndex++;
    }
    public void LifeIncrement()
    {
        currentLife++;
    }
    public void LifeDecrement()
    {
        currentLife--;
    }
    #endregion
}
