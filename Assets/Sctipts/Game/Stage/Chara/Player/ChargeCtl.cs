using UnityEngine;

public class ChargeCtl : MonoBehaviour
{
    [SerializeField] private TriggerCtl groundTrigger;
    [Header("チャージ設定")]
    [SerializeField] AnimationCurve chargeCurve;

    private float _time = 0f;
    private float _charge = 0f;
    private bool _charging = false;
    private bool _seDone = false;

    public float ChargeTime => _time;
    public float Charge => _charge;
    public bool Charging => _charging;

    /// <summary>
    /// チャージを開始するメソッド
    /// </summary>
    public void ChargeStart()
    {
        _time = 0f;
        _charging = true;
        _seDone = false;
        if (groundTrigger.IsActive)
        {
            SoundMgr.instance.Play(SEkey.Charge);
        }
    }
    /// <summary>
    /// チャージを終了するメソッド
    /// </summary>
    public void ChargeFinish()
    {
        _charging = false;
        _time = 0f;
        SoundMgr.instance.Stop(SEkey.Charge);
    }

    private void Update()
    {
        if (_charging && groundTrigger.IsActive)
        {
            _time += Time.deltaTime;
            if (!_seDone)
            {
                SoundMgr.instance.Play(SEkey.Charge);
                _seDone = true;
            }
        }
        _charge = chargeCurve.Evaluate(_time);
    }
}
