using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuCtl : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    private InputSystem_Actions _inputActions;
    private InputCtl _inputCtl; // InputCtlの参照

    public InputSystem_Actions InputActions { set { _inputActions = value; } } // 外部から受け取る
    public InputCtl InputCtl { set { _inputCtl = value; }  }

    private void Start()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false); // メニューを非表示で初期化する
        }
        if (_inputActions != null)
        {
            _inputActions.Game.Menu.started += OpenMenu;
        }
        else
        {
            Debug.Log("MenuCtlにInputCtlが渡されていません．");
        }
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        SoundMgr.instance.Play(SEkey.Menu);
        if (_inputCtl != null)
        {
            _inputCtl.SetPlayerActions(false);
        }
        else
        {
            throw new NullReferenceException("MenuCtlにInputCtlが渡されていません．");
        }
        menuPanel.SetActive(true); // メニューを表示する
        if (_inputActions != null)
        {
            _inputActions.Game.Menu.started -= OpenMenu;
            _inputActions.Game.Menu.started += CloseMenu;
        }
        else
        {
            Debug.Log("MenuCtlにInputCtlが渡されていません．");
        }

        Time.timeScale = 0f; // ゲームを一時停止する
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        SoundMgr.instance.Play(SEkey.Menu);
        menuPanel.SetActive(false); // メニューを非表示にする
        Time.timeScale = 1f; // ゲームを再開する
        if (_inputCtl != null)
        {
            _inputCtl.SetPlayerActions(true);
        }
        if (_inputActions != null)
        {
            _inputActions.Game.Menu.started -= CloseMenu;
            _inputActions.Game.Menu.started += OpenMenu;
        }
        else
        {
            Debug.Log("MenuCtlにInputCtlがアタッチされていません．");
        }
    }
}
