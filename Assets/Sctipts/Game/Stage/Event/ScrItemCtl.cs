using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrItemCtl : MonoBehaviour
{
    [SerializeField] private int SCORE = 10; // スコアの加算値
    private int _index = 0;
    [SerializeField] private SEkey se;
    private TriggerCtl trggrCtl;
    private int _stageNum = 0;

    public int Index { set { _index = value; } }

    private void Awake()
    {
        trggrCtl = GetComponent<TriggerCtl>();
        if (trggrCtl == null)
        {
            throw new NullReferenceException("TriggerCtl が見つかりません.");
        }
        trggrCtl.TriggerEnterAction += AddScore;
        _stageNum = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void OnDisable()
    {
        trggrCtl.TriggerEnterAction -= AddScore;
    }

    /// <summary>
    /// スコアを加算し、アイテムを破棄する
    /// </summary>
    private void AddScore()
    {
        SoundMgr.instance.Play(se);
        GmMgr.Instance.ItemSituation[_stageNum-1][_index] = false;
        GmMgr.Instance.Score.Add(SCORE);
        Destroy(gameObject);
    }
}
