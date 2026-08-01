using UnityEngine;

public class Charge_viw : MonoBehaviour
{
    [SerializeField] Input_ctl _input; // 入力クラス

    private Vector3 _arrowPos = Vector3.zero; // 矢印の位置ベクトル
    private float _distance; // プレイヤーから矢印までの距離
    private float _angle; // 矢印の角度
    private float _scale; // 矢印の大きさ

    const float _distanceFromPlayer = 2f; // プレイヤーからの距離
    const float _maxScale = 1.25f; // 矢印の最大大きさ
    const float _minScale = 0.5f; // 矢印の最小大きさ

    void Start()
    {
        _arrowPos.z = 0f;
    }
    
    void Update()
    {
        CalcScale();
        CalcPos();
        CalcAngle();
        ShowArrow();
    }

    void ShowArrow()
    {
        _distance = _distanceFromPlayer + _scale * 2;
        transform.localPosition = _arrowPos;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    void CalcPos()
    {
        float scaleX = _input.PointerRight ? 1f : -1f; // 右向きなら正、左向きなら負
        _arrowPos.x = scaleX * _distance * Mathf.Cos(_input.PointerAngle * Mathf.Deg2Rad);
        _arrowPos.y = _distance * Mathf.Sin(_input.PointerAngle * Mathf.Deg2Rad);
    }

    void CalcAngle()
    {
        float rotateX = _input.PointerRight ? 0f : 180f; // 左向きなら180度回転
        _angle = _input.PointerAngle - rotateX;
        
    }

    void CalcScale()
    {
        _scale = Mathf.Lerp(_minScale, _maxScale, _input.BtnHoldTime / 1f); // 1秒で最大スケールに到達
        transform.localScale = new Vector3(_scale, 0.5f, 1f);
    }
}
