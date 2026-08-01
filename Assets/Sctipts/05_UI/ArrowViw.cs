using UnityEngine;

public class ArrowViw : MonoBehaviour
{
    [SerializeField] JumpCtl jumpCtl;
    [SerializeField] ChargeCtl chargeCtl;

    const float DISTANCE_FROM_PLAYER = 2f; // プレイヤーからの距離
    const float MAX_SCALE = 1.25f; // 矢印の最大大きさ
    const float MIN_SCALE = 0.5f; // 矢印の最小大きさ

    private Vector2 _arrowPos = Vector2.zero; // 矢印の位置ベクトル
    private float _scale; // 矢印の大きさ
    private float _distance; // プレイヤーから矢印までの距離
    
    private void Update()
    {
        CalcScale();
        CalcPosition();
        CalcAngle();
    }

    private void CalcScale()
    {
        _scale = Mathf.Lerp(MIN_SCALE,MAX_SCALE, chargeCtl.ChargeTime);
        transform.localScale = new Vector3(_scale, 0.5f, 1f);
    }
    private void CalcPosition()
    {
        _distance = DISTANCE_FROM_PLAYER + _scale * 2;
        transform.localPosition = _distance * jumpCtl.JumpVector;
    }
    private void CalcAngle()
    {
        float angle = Mathf.Atan2(jumpCtl.JumpVector.y, jumpCtl.JumpVector.x);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
}