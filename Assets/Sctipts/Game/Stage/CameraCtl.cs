using UnityEngine;

public class CameraCtl : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] TriggerCtl _triggerGround;
    [SerializeField] AnimationCurve _cameraCurve;

    private Vector3 _cameraPos;
    private float _groundingTime = 0f;

    [SerializeField] private float _cameraMinX = -6f; // カメラの最小X座標
    [SerializeField] private float _cameraMaxX = 187f; // カメラの最大X座標
    const float _cameraOffsetY = 2f; // カメラのY座標のオフセット

    void Start()
    {
        _cameraPos = transform.position;
        _cameraPos.z = transform.position.z; // カメラの初期Z座標を保存
        _triggerGround.TriggerEnterAction += OnEnterPos;
    }

    void Update()
    {
        _groundingTime += Time.deltaTime;
    }

    void LateUpdate()
    {
        CalcCameraPosition();
        SetCameraPosition();
    }

    private void OnDisable()
    {
        _triggerGround.TriggerEnterAction -= OnEnterPos;
    }

    void SetCameraPosition() // カメラの位置を更新
    {
        transform.position = _cameraPos;
    }

    void CalcCameraPosition() // カメラの位置を計算するための関数
    {
        CalcCameraPosX();
        CalcCameraPosY();
    }

    #region //カメラの位置を計算するための関数群
    /// <summary>
    /// プレイヤーのX座標に基づいてカメラのX座標を計算するための関数
    /// </summary>
    void CalcCameraPosX()
    {
        float x;
        x = Mathf.Clamp(_player.transform.position.x, _cameraMinX, _cameraMaxX);
        _cameraPos.x = x; // プレイヤーのX座標をカメラの範囲内に制限
    }

    // プレイヤーのY座標に基づいてカメラのY座標を計算するための内部変数
    private float _from = 0f;
    private float _to = 0f;

    void OnEnterPos()
    {
        _from = _cameraPos.y; // カメラのY座標を保存
        _to = _player.transform.position.y + _cameraOffsetY; // プレイヤーのY座標にオフセットを加えた値を保存
        _groundingTime = 0f; // 接地入りした瞬間に時間をリセット
    }

    /// <summary>
    /// プレイヤーのY座標に基づいてカメラのY座標を計算するための関数
    /// </summary>
    void CalcCameraPosY()
    {
        if (_triggerGround.IsActive)
        {
            float deltaY = _to - _from;
            if (deltaY > 2f || deltaY < -2f) // プレイヤーのY座標がカメラのY座標から大きく離れた場合のみカメラを移動
            {
                _cameraPos.y = _from + deltaY * _cameraCurve.Evaluate(_groundingTime); // カメラのY座標をプレイヤーのY座標に近づける
            }
        }
    }
    #endregion
}
