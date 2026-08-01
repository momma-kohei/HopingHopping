using UnityEngine;

/// <summary>
/// ゲーム全体を通して共有するデータや操作を扱うクラス．
/// プレハブ化によりシングルトンで実装し，静的にアクセスできるようにする．
/// </summary>
public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }
    public SceneCtl SceneCtl { get; private set; }
    public InfoMdl InfoMdl { get; private set; }
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
        // コンポーネントを読み込む
        SceneCtl = GetComponent<SceneCtl>();
        InfoMdl = GetComponent<InfoMdl>();
    }
}
