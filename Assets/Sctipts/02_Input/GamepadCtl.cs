using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ゲームパッド操作を制御するクラス
/// 1. ボタン操作を設定する
/// 2. スティックの角度から単位ベクトルを取得する
/// </summary>
public class GamepadCtl : MonoBehaviour
{
    private bool _isActive = false;
    private Vector2 _stickVector = Vector2.zero; // スティックのベクトル
    private VectorMdl _vectorMdl;
    private InputSystem_Actions _inputActions; // 入力システムのアクションを管理するクラス

    public bool IsActive { set { _isActive = value; } }
    public Vector2 StickrVector => _stickVector;
    public InputSystem_Actions InputActions { set { _inputActions = value; } } // 外部から受け取る
    public Action BtnDownAction; // ボタンを押したときのアクション
    public Action BtnUpAction; // ボタンを離したときのアクション

    #region// unityイベント関数
    private void Start()
    {
        _vectorMdl = new VectorMdl();
        if (_inputActions == null)
        {
            throw new Exception("InputSystem_Actionが設定されていません．");
        }

        SetMouseAction(true);
    }
    private void Update()
    {
        if (_isActive)
        {
            CalcStickVector();
        }
    }
    private void OnDestroy()
    {
        SetMouseAction(false);
    }
    #endregion

    private void CalcStickVector()
    {
        if (Gamepad.current.leftStick.ReadValue() == Vector2.zero) return; // ゲームパッドのスティックがニュートラルのときはそのまま
        _stickVector = _vectorMdl.CalcUnitVentor(Gamepad.current.leftStick.ReadValue()); // スティックの単位ベクトルを取得
    }

    private void SetMouseAction(bool on)
    {
        if (on)
        {
            _inputActions.Game.Click.started += OnBtnDown;
            _inputActions.Game.Click.canceled += OnBtnUp;
        }
        else
        {
            _inputActions.Game.Click.started -= OnBtnDown;
            _inputActions.Game.Click.canceled -= OnBtnUp;
        }
    }
    private void OnBtnDown(InputAction.CallbackContext context)
    {
        if (_isActive)
        {
            BtnDownAction?.Invoke();
        }
    }
    private void OnBtnUp(InputAction.CallbackContext context)
    {
        if (_isActive)
        {
            BtnUpAction?.Invoke();
        }
    }
}
