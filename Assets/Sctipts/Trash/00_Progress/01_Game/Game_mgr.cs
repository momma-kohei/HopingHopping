using UnityEngine;
using System;
using Status;

/// <summary>
/// ゲーム全体を通して共有するデータや操作を扱うクラス．
/// プレハブ化によりシングルトンで実装し，静的にアクセスできるようにする．
/// </summary>
public class Game_mgr : MonoBehaviour
{
    #region// GameManager自体に関する処理
    public static Game_mgr instance = null;
    private void Singleton() // シングルトン化を行う処理
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region// GameManagerが持つ情報に関する処理
    public Stages stages = new Stages();
    public Lives lives = new Lives();
    private void CheckClasses() // クラスのヌルチェック処理
    {
        if (stages == null) throw new NullReferenceException("ステージクラスが不正です．");
        if (lives == null) throw new NullReferenceException("ライフクラスが不正です．");
    }
    #endregion

    #region// Unityイベント関数
    void Awake()
    {
        Singleton();
        CheckClasses();
    }
    #endregion
}
