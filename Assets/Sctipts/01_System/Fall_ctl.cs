using System;
using UnityEngine;

public class Fall_ctl : MonoBehaviour
{
    [SerializeField] Stage_ctl _stage_ctl;
    private Trigger_ctl _trigger_ctl;

    void Start()
    {
        _trigger_ctl = GetComponent<Trigger_ctl>();
        CheckNull();
        _trigger_ctl.OnEnter += Main;
    }

    private void OnDisable()
    {
        _trigger_ctl.OnEnter -= Main;
    }

    void Main()
    {
        _stage_ctl.Fall();
        _trigger_ctl.ResetFlag();
    }

    void CheckNull()
    {
        if (_stage_ctl == null) throw new Exception("Stage_ctlがアタッチされていません．");
        if (_trigger_ctl == null) throw new Exception("Trigger_ctlコンポーネントが見つかりません．");
        if (GameManager.instance == null) throw new Exception("GameManagerが見つかりません．");
    }
}
