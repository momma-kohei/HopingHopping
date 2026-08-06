using System;
using UnityEngine;

public class GravityCtl : MonoBehaviour
{
    const float MAX_VELOCITY = 100f; // 落下速度上限
    const float MAX_VELOCITY_WATER = 20f;
    const float UPWARD_GRAVITY = 1f; // 上昇中の重力倍率
    const float APEX_GRAVITY = 0.7f; // 頂点付近の重力倍率
    const float DOWNWARD_GRAVITY = 2.5f; // 落下中の重力倍率
    const float BASIC_GRAVITY_SCALE = 30f; // 基準の重力スケール

    [SerializeField] private TriggerCtl inWater = null;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        if (_rb2d == null) throw new NullReferenceException("Rigidbody2Dが見つかりません．");
    }

    private void FixedUpdate()
    {
        CalcGravityScale();
        LimitVelocityY();
    }

    private void CalcGravityScale() // GravityScaleを取得するメソッド
    {
        float gravityScaleRatio = 1f;
        float vy = _rb2d.linearVelocityY;
        if (vy > 0.1f)
        {
            gravityScaleRatio = UPWARD_GRAVITY;
        }
        else if (vy < -0.1f)
        {
            gravityScaleRatio = DOWNWARD_GRAVITY;
        }
        else
        {
            gravityScaleRatio = APEX_GRAVITY;
        }
        _rb2d.gravityScale = BASIC_GRAVITY_SCALE * gravityScaleRatio;
    }

    private void LimitVelocityY() // 速度上限を適用するメソッド
    {
        float vy = _rb2d.linearVelocityY;
        if (inWater != null && inWater.IsActive)
        {
            if (vy < -MAX_VELOCITY_WATER)
            {
                vy = -MAX_VELOCITY_WATER;
            }
            _rb2d.linearVelocityY = vy;
            return;
        }
        else
        {
            if (vy < -MAX_VELOCITY)
            {
                vy = -MAX_VELOCITY;
            }
            _rb2d.linearVelocityY = vy;
        }
    }
}
