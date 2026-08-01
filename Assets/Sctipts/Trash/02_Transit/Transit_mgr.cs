using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transit_mgr : MonoBehaviour
{
    #region// インスペクターで見せる情報
    [Header("次のシーン名")]
    [SerializeField] private Scenes _nextSceneName = Scenes.None;
    [Header("シーン遷移の秒数")]
    [SerializeField] private float _delaySecond = 1f;
    #endregion
    #region// privateな処理
    public enum Scenes
    {
        None,
        Title,
        Select,
        Stage1,
        Stage2,
        Stage3,
        Stage4
        
    }
    private void GoToNextScene()
    {
        if (_nextSceneName == Scenes.None)
        {
            //throw new NullReferenceException("次のシーン名が不正です．");
            Debug.Log("次のシーンが設定されていません．");
            return;
        }
        SceneManager.LoadScene(_nextSceneName.ToString());
    }
    #endregion
    #region// publicな処理
    /// <summary>
    /// シーン遷移を遅延させる秒数
    /// </summary>
    public float DelaySecond { get { return _delaySecond; } }
    /// <summary>
    /// シーン遷移イベント
    /// </summary>
    public event Action Transition;
    /// <summary>
    /// シーン遷移を行う時に呼ぶメソッド
    /// </summary>
    public void OnTransition()
    {
        Transition.Invoke();
        Invoke(nameof(GoToNextScene), _delaySecond); // 指定時間後にシーン遷移
    }
    /// <summary>
    /// 次のシーンを設定するメソッド
    /// </summary>
    /// <param name="next">次のシーン</param>
    public void SetNextScene(Scenes next)
    {
        _nextSceneName = next;
    }
    #endregion
}
