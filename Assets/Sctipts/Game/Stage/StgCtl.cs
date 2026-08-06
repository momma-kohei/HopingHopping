using UnityEngine;
using UnityEngine.SceneManagement;

public class StgCtl : MonoBehaviour
{
    public static StgCtl Instance { get; private set; }

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private TriggerCtl goal;

    private bool _isPlayable = false;
    public bool IsPlayable => _isPlayable;

    private void Start()
    {
        Instance = this;
        _isPlayable = true;
        int _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SoundMgr.instance.Play((BGMkey) _sceneIndex);
    }

    /// <summary>
    /// プレイヤーをスタート地点にリスポーンさせる
    /// </summary>
    public void Respawn()
    {
        player.transform.position = startPoint.transform.position;
    }

    /// <summary>
    /// ゲームオーバー処理を行う
    /// </summary>
    public void GameOver()
    {
        ScnMgr.Instance.TransitScene(SceneType.GameOver);
    }
}
