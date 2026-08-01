using Stage_mdl;
using UnityEngine;

/// <summary>
/// ステージシーン内の情報で完結する管理を行うクラス
/// </summary>
public class Stage_ctl : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _checkPoint;
    [SerializeField] private Trigger_ctl _goal;

    public LifeClass life;
    public ScoreClass score;

    private bool _isPlayable = false;
    public bool IsPlayable => _isPlayable;

    void Awake()
    {
        life = new LifeClass();
        score = new ScoreClass();
    }

    void Start()
    {
        CheckNull();
        _isPlayable = true;
        Respawn();
    }

    public void Respawn()
    {
        int point = GameManager.instance.stage.GetCheckPoint();
        if (point >= _checkPoint.Length) throw new System.NullReferenceException("チェックポイントのインデックスが無効です．");
        _player.transform.position = _checkPoint[point].transform.position;
    }

    public void Fall()
    {
        life.Decliment();
        if (life.Get() > 0)
        {
            Respawn();
        }
        else
        {
            score.Reset();
            GameManager.instance.GameOver();
        }
    }

    public void SwitchPlayable()
    {
        _isPlayable = !_isPlayable;
    }

    void CheckNull()
    {
        if (_player == null) throw new System.NullReferenceException("プレイヤーが設定されていません．");
        if (_checkPoint == null || _checkPoint.Length <= 0) throw new System.NullReferenceException("スタート地点が設定されていません．");
        if (_goal == null) throw new System.NullReferenceException("ゴールが設定されていません．");
        if (GameManager.instance == null) throw new System.NullReferenceException("GameManagerが存在しません．");
    }
}
