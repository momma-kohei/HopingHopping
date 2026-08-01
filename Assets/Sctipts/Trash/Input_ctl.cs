using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_ctl : MonoBehaviour
{
    [SerializeField] Transform _playerTfm;

    const float _minAngle = 30f; // 最小角度

    private Vector2 _pointerVector = Vector2.zero; // ポインターのベクトル
    private float _pointerAngle = 0f; // ポインターの指示角度
    private bool _pointerRight = true; // ポインターがプレイヤーの右側を指しているか
    private float _btnHoldTime = 0f; // ボタンを押している時間
    private bool _isGamepad = false; // ゲームパッドを使用しているか
    private InputSystem_Actions _inputActions; // 入力システムのアクションを管理するクラス


    // 外部からアクセスできる情報
    public float PointerAngle => _pointerAngle;
    public bool PointerRight => _pointerRight;
    public float BtnHoldTime => _btnHoldTime;

    public Action BtnDownAction; // ボタンを押したときのアクション
    public Action BtnUpAction; // ボタンを離したときのアクション


    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        
        _inputActions.Game.Click.started += OnBtnDown;
        _inputActions.Game.Click.canceled += OnBtnUp;

        // 入力システムのアクションが変化したときのイベントにメソッドを登録
        InputSystem.onActionChange += (obj, change) =>
        {
            if (change == InputActionChange.ActionStarted)
            {
                var action = (InputAction)obj;
                if (action.activeControl != null)
                {
                    CheckDevice(action.activeControl.device); // 最後に使われたデバイスを確認
                }
            }
        };
    }
    
    void Update()
    {
        CalcInnerVariantes(); // 内部の変数を計算
    }

    void OnDestroy()
    {
        // InputActionのコールバックの解除
        _inputActions.Game.Click.started -= OnBtnDown;
        _inputActions.Game.Click.canceled -= OnBtnUp;
        _inputActions.Dispose(); // InputActionAssetのラッパークラスの破棄
    }

    void OnEnable()
    {
        _inputActions.Enable(); // 全体のActionを有効化
    }

    void OnDisable()
    {
        _inputActions.Disable(); // 全体のActionを無効化
    }

    void CheckDevice(InputDevice device) // デバイスの種類によってフラグを切り替え
    {
        if (device is Gamepad)
        {
            _isGamepad = true;
        }
        else if (device is Mouse || device is Touchscreen)
        {
            _isGamepad = false;
        }
    }

    void OnBtnDown(InputAction.CallbackContext context)
    {
        BtnDownAction?.Invoke(); // ボタンを押したときのアクションを呼び出す
    }

    void OnBtnUp(InputAction.CallbackContext context)
    {
        BtnUpAction?.Invoke(); // ボタンを離したときのアクションを呼び出す
    }

    void CalcInnerVariantes()
    {
        CalcPointerVector(); // ポインターのベクトルを計算
        CalcPointerAngle(); // ポインターの角度を計算
        CalcPointerRight(); // ポインターがプレイヤーの右側を指しているかを計算
        CalcBtnHoldTime(); // ボタンを押している時間を計算
    }

    #region // 内部の変数を計算するメソッド群
    void CalcPointerAngle()
    {
        _pointerAngle = LimitAngle(CalcVectorAngle(_pointerVector)); // ポインターのベクトル角度を計算
    }

    void CalcPointerRight()
    {
        _pointerRight = _pointerVector.x >= 0f; // ポインターがプレイヤーの右側を指しているか
    }

    void CalcPointerVector()
    {
        Vector2 playerVector = new Vector2(_playerTfm.position.x, _playerTfm.position.y); // プレイヤーの足元座標を取得
        if (_isGamepad)
        {
            if (Gamepad.current.leftStick.ReadValue() == Vector2.zero) return; // ゲームパッドのスティックがニュートラルのときはそのまま
            _pointerVector = Gamepad.current.leftStick.ReadValue();
        }
        if (!_isGamepad)
        {
            _pointerVector = _inputActions.Game.Pointer.ReadValue<Vector2>(); // ポインターの座標を取得
            _pointerVector = Camera.main.ScreenToWorldPoint(_pointerVector); // ポインターの座標をワールド座標に変換
            _pointerVector -= playerVector; // gamepad以外の場合はプレイヤーからの相対ベクトルに変換
        }
    }

    float CalcVectorAngle(Vector2 vector) // ベクトルの角度を計算するメソッド
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    float LimitAngle(float angle) // 角度を制限するメソッド
    {
        if (angle < _minAngle && angle > -90f) return _minAngle;
        else if (angle < -90 || angle > 180 - _minAngle) return 180f - _minAngle;
        return angle;
    }

    void CalcBtnHoldTime() // ボタンを押している時間を計算するメソッド
    {
        if (_inputActions.Game.Click.IsPressed())
        {
            _btnHoldTime += Time.deltaTime; // ボタンを押している時間を加算
        }
        else
        {
            _btnHoldTime = 0f; // ボタンを離したときにリセット
        }
    }
    #endregion
}
