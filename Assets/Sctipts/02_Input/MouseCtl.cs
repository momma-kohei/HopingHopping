using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// マウス操作を制御するクラス
/// 1. クリック操作を設定する
/// 2. マウスの位置から単位ベクトルを取得する
/// </summary>
public class MouseCtl : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private bool _isActive = true;
    private Vector2 _pointerVector = Vector2.zero; // ポインターの相対ベクトル
    private VectorMdl _vectorMdl;
    private InputSystem_Actions _inputActions; // 入力システムのアクションを管理するクラス

    public bool IsActive { set { _isActive = value; } }
    public Vector2 PointerVector => _pointerVector;
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
            CalcPointerUnitVector();
        }
    }
    private void OnDestroy()
    {
        SetMouseAction(false);
    }
    #endregion

    private void CalcPointerUnitVector()
    {
        Vector2 playerVector = new Vector2(playerTransform.position.x, playerTransform.position.y); // プレイヤーの座標を取得
        Vector2 pointerWorldPosition = Camera.main.ScreenToWorldPoint(_inputActions.Game.Pointer.ReadValue<Vector2>()); // ポインターのワールド座標を取得
        _pointerVector = _vectorMdl.CalcUnitVentor(pointerWorldPosition - playerVector); // プレイヤー -> ポインターの単位ベクトルを取得
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
