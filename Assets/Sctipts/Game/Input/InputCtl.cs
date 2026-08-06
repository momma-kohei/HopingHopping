using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCtl : MonoBehaviour
{
    [SerializeField] private JumpCtl jumpCtl;
    private MouseCtl mouseCtl;
    private GamepadCtl gamepadCtl;
    private InputSystem_Actions _inputActions;

    private InputDev _inputDev;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
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

        mouseCtl = GetComponent<MouseCtl>();
        if (mouseCtl == null)
        {
            throw new Exception("MouseControllerが見つかりません．");
        }
        mouseCtl.InputActions = _inputActions;

        gamepadCtl = GetComponent<GamepadCtl>();
        if (gamepadCtl == null)
        {
            throw new Exception("GamepadControllerが見つかりません．");
        }
        gamepadCtl.InputActions = _inputActions;
    }

    private void Start()
    {
        if (jumpCtl == null)
        {
            throw new Exception("JumpControllerがアタッチされていません．");
        }

        SetPlayerActions(true);
    }
    private void Update()
    {
        SwitchInput();
        SetVector();
    }
    private void OnEnable() 
    {
        _inputActions.Enable();
    }
    private void OnDestroy()
    {
        // InputActionのコールバックの解除
        _inputActions.Dispose(); // InputActionAssetのラッパークラスの破棄
    }
    private void OnDisable()
    {
        _inputActions.Disable();
        SetPlayerActions(false);
    }

    private void CheckDevice(InputDevice device) // デバイスの種類によってフラグを切り替え
    {
        if (device is Gamepad)
        {
            _inputDev = InputDev.Gamepad;
        }
        else if (device is Mouse)
        {
            _inputDev = InputDev.Mouse;
        }
    }

    private void SwitchInput()
    {
        switch (_inputDev)
        {
            case InputDev.Gamepad:
                gamepadCtl.IsActive = true;
                mouseCtl.IsActive = false;
                break;
            case InputDev.Mouse:
                gamepadCtl.IsActive = false;
                mouseCtl.IsActive = true;
                break;
        }
    }
    private void SetVector()
    {
        switch (_inputDev) 
        {
            case InputDev.Gamepad:
                jumpCtl.JumpVector = gamepadCtl.StickrVector;
                break;
            case InputDev.Mouse:
                jumpCtl.JumpVector = mouseCtl.PointerVector;
                break;
        }
    }

    public void SetPlayerActions(bool on)
    {
        if (on)
        {
            mouseCtl.BtnDownAction   += jumpCtl.Charge;
            mouseCtl.BtnUpAction     += jumpCtl.Jump;
            gamepadCtl.BtnDownAction += jumpCtl.Charge;
            gamepadCtl.BtnUpAction   += jumpCtl.Jump;
        }
        else
        {
            mouseCtl.BtnDownAction   -= jumpCtl.Charge;
            mouseCtl.BtnUpAction     -= jumpCtl.Jump;
            gamepadCtl.BtnDownAction -= jumpCtl.Charge;
            gamepadCtl.BtnUpAction   -= jumpCtl.Jump;
        }
    }
}

public enum InputDev
{
    Mouse,
    Gamepad,
}
