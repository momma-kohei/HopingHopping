using UnityEngine;

public class StageBlind : MonoBehaviour
{
    private Vector3 _pos;
    private float[] _blindPosList = { 319f, 607f, 1033f, 1280f };

    void Start()
    {
        _pos = transform.localPosition; // 初期位置を保持
        SetBlindPos(); // 位置を更新
    }

    private void SetBlindPos()
    {
        _pos.x = _blindPosList[Mathf.Clamp(GmMgr.Instance.Checker.Value, 0, 3)]; // チェックポイントの値に応じてブラインドの位置を設定
        transform.localPosition = _pos; // x座標を更新
    }
}
