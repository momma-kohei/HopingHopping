using UnityEngine;
using Stage_mdl;

/// <summary>
/// シーンを跨ぐような管理を行うクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public StageClass stage;

    private int _stageNum = 0;
    public int StageNum {  get { return _stageNum; } set { _stageNum = value; } }

    void Awake()
    {
        
        stage = new StageClass();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ゲームオーバーの処理
    /// </summary>
    public void GameOver()
    {
        stage.ResetPoint(stage.GetStage());
        Debug.Log("Game Over");
    }

    /// <summary>
    /// ゲームクリア時の処理
    /// </summary>
    public void GameClear()
    {
        Debug.Log("Game Clear");
    }
}
