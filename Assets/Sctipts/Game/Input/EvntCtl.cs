using System;
using UnityEngine;

public class EvntCtl : MonoBehaviour
{
    /// <summary>
    /// ポインターがクリックされたときに呼び出されるイベント
    /// </summary>
    public event Action PointerClickEvent;
    public void OnPointerClicked()
    {
        PointerClickEvent?.Invoke();
    }

    /// <summary>
    /// ポインターがオブジェクトに入ったときに呼び出されるイベント
    /// </summary>
    public event Action PointerEnterEvent;
    public void OnPointerEnter()
    {
        PointerEnterEvent?.Invoke();
    }

    /// <summary>
    /// ポインターがオブジェクトから出たときに呼び出されるイベント
    /// </summary>
    public event Action PointerExitEvent;
    public void OnPointerExit()
    {
        PointerExitEvent?.Invoke();
    }
}
