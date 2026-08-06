using System;
using UnityEngine;

public class GoalCtl : MonoBehaviour
{
    [SerializeField] private int stageNum; // ステージ番号
    [SerializeField] private GoalPanel goalPanel; // クリアパネルの参照
    [SerializeField] private InputCtl inputCtl;

    TriggerCtl triggerCtl;

    private void Awake()
    {
        triggerCtl = GetComponent<TriggerCtl>();
        if (triggerCtl == null)
        {
            throw new NullReferenceException("TriggerCtl が見つかりません.");
        }

        triggerCtl.TriggerEnterAction += OnGoal;
    }

    private void OnDisable()
    {
        triggerCtl.TriggerEnterAction -= OnGoal;
    }

    private void OnGoal()
    {
        inputCtl.SetPlayerActions(false); // プレイヤーの操作を無効化
        Debug.Log("Stage Clear!");
        StartCoroutine(goalPanel.ShowResult());
        if (GmMgr.Instance.Checker.Value < stageNum)
        {
            GmMgr.Instance.Checker.Increase(); // ステージクリア数を増やす
        }
    }
}
