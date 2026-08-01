using System;
using UnityEngine;

public class Score_ctl : MonoBehaviour
{
    private Trigger_ctl _trigger_ctl;
    const int _scorePreItem = 100;

    [SerializeField] private Stage_ctl _stage_ctl;

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
        _stage_ctl.score.Add(_scorePreItem);
        Destroy(gameObject);
    }

    void CheckNull()
    {
        if (_trigger_ctl == null) throw new Exception("Trigger_ctlコンポーネントが見つかりません．");
        if (_stage_ctl == null) throw new Exception("Stage_ctlが見つかりません．");
    }
}
