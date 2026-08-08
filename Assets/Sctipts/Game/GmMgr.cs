using Define;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲームという枠組みを管理するクラス
/// 今回は，スコア，リミット（時間や体力），チェックポイントを核として考える
/// </summary>
public class GmMgr : MonoBehaviour
{
    public static GmMgr Instance { get; private set; } // シングルトンのインスタンスを保持するプロパティ
    /// <summary>
    /// スコアを扱うクラス
    /// </summary>
    public DiscreteValue Score { get; private set; }
    /// <summary>
    /// 体力を扱うクラス
    /// </summary>
    public DiscreteValue Life { get; private set; }
    /// <summary>
    /// チェックポイントを扱うクラス
    /// </summary>
    public DiscreteValue Checker { get; private set; }

    public List<List<bool>> ItemSituation = new List<List<bool>>();

    public int FinalScore { get { return Score.Value + (Life.Value * 20); } }
    public int BestScore { get { return PlayerPrefs.GetInt("BestScore", 0); } }

    private void Awake()
    {
        // シングルトン化を行う
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも破壊されない
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    public void Init()
    {
        ItemSituation.Clear();

        // スコア，体力，チェックポイントの初期化
        Score = new DiscreteValue(0, 0, 999, 10);
        Life = new DiscreteValue(4, 0, 99, 1);
        Checker = new DiscreteValue(0, 0, 10, 1);

        for (int i = 0; i < 4; i++)
        {
            ItemSituation.Add(new List<bool>());
        }
    }

    /// <summary>
    /// ベストスコアを保存するメソッド
    /// </summary>
    public void SaveBestScore()
    {
        if (FinalScore > BestScore)
        {
            PlayerPrefs.SetInt("BestScore", FinalScore);
        }
    }
}
