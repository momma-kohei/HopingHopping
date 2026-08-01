using UnityEngine;

public class ChargeCtl : MonoBehaviour
{
    [Header("チャージ設定")]
    [SerializeField] AnimationCurve chargeCurve;

    private float _time = 0f;
    private float _charge = 0f;
    private bool _charging = false;

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
        SoundMgr.instance.Play(SEkey.Charge);
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
        if (_charging)
        {
            _time += Time.deltaTime;
        }
        _charge = chargeCurve.Evaluate(_time);
    }
}
