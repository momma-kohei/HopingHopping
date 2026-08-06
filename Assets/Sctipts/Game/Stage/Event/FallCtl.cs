using System;
using UnityEngine;

public class FallCtl : MonoBehaviour
{
    [SerializeField] private FallPanel fallPanel; // フォールパネルの参照
    [SerializeField] private InputCtl inputCtl;
    [SerializeField] private Rigidbody2D player;

    private TriggerCtl triggerCtl;

    private void Awake()
    {
        triggerCtl = GetComponent<TriggerCtl>();
        if (triggerCtl == null)
        {
            throw new NullReferenceException("TriggerCtl が見つかりません.");
        }
        triggerCtl.TriggerEnterAction += OnFall;
    }

    private void OnDisable()
    {
        triggerCtl.TriggerEnterAction -= OnFall;
    }

    private void OnFall()
    {
        SoundMgr.instance.Play(SEkey.Fall); // 落下の効果音を再生
        player.linearVelocityX = 0; // プレイヤーの水平速度を防止
        GmMgr.Instance.Life.Decrease();
        if ( GmMgr.Instance.Life.Value > 0)
        {
            inputCtl.SetPlayerActions(false); // プレイヤーの操作を無効化
            StartCoroutine(fallPanel.ShowRetry());
        }
        else
        {
            StgCtl.Instance.GameOver();
        }
        //triggerCtl.ResetActivity();
    }
}
