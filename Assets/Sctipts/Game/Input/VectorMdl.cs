using UnityEngine;

/// <summary>
/// プレイや＝操作における
/// ベクトルの計算に用いるメソッドをまとめたクラス
/// </summary>
public class VectorMdl
{
    const float MIN_ANGLE = 30f;

    /// <summary>
    /// 角度制限を付与された単位ベクトルに変換するメソッド
    /// </summary>
    /// <param name="vector">入力ベクトル</param>
    /// <returns>変換後ベクトル</returns>
    public Vector2 CalcUnitVentor(Vector2 vector)
    {
        float angle = LimitAngle(CalcVectorAngle(vector));
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    private float CalcVectorAngle(Vector2 vector) // ベクトルの角度を計算するメソッド
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    private float LimitAngle(float angle) // 角度を制限するメソッド
    {
        if (angle < MIN_ANGLE && angle > -90f) return MIN_ANGLE;
        else if (angle < -90 || angle > 180 - MIN_ANGLE) return 180f - MIN_ANGLE;
        return angle;
    }
}
