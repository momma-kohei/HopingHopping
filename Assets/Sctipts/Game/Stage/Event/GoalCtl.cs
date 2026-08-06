using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCtl : MonoBehaviour
{
    private int _stageNum; // ステージ番号
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

        _stageNum = SceneManager.GetActiveScene().buildIndex - 1;
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
        if (GmMgr.Instance.Checker.Value < _stageNum)
        {
            GmMgr.Instance.Checker.Increase(); // ステージクリア数を増やす
            for (int i = 0; i < 4 + _stageNum; i++)
            {
                GmMgr.Instance.Life.Increase();
            }
        }
    }
}
