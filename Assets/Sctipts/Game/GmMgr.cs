using Define;
using UnityEngine;

/// <summary>
/// ゲームという枠組みを管理するクラス
/// 今回は，スコア，リミット（時間や体力），チェックポイントを核として考える
/// </summary>
public class GmMgr : MonoBehaviour
{
    public static GmMgr Instance { get; private set; } // シングルトンのインスタンスを保持するプロパティ
    public DiscreteValue Score { get; private set; } // スコアを扱うクラス
    public DiscreteValue Life { get; private set; } // 体力を扱うクラス
    public DiscreteValue Checker { get; private set; } // チェックポイントを扱うクラス
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

        // スコア，体力，チェックポイントの初期化
        Score = new DiscreteValue(0, 999, 10);
        Life = new DiscreteValue(0, 99, 1);
        Checker = new DiscreteValue(0, 10, 1);
    }
}
