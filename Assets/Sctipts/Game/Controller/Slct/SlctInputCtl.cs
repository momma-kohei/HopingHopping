using UnityEngine;

public class SlctInputCtl : MonoBehaviour
{
    [SerializeField] private MenuCtl menuCtl;

    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();

        if (menuCtl != null)
        {
            menuCtl.InputActions = _inputActions;
        }
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
    }


}
