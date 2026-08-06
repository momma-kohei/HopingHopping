using System;
using UnityEngine;

public class ScrItemCtl : MonoBehaviour
{
    [SerializeField] private int SCORE = 10; // スコアの加算値
    private TriggerCtl trggrCtl;

    private void Awake()
    {
        trggrCtl = GetComponent<TriggerCtl>();
        if (trggrCtl == null)
        {
            throw new NullReferenceException("TriggerCtl が見つかりません.");
        }
        trggrCtl.TriggerEnterAction += AddScore;
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
        GmMgr.Instance.Score.Add(SCORE);
        Destroy(gameObject);
    }
}
