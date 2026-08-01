using System;
using UnityEngine;

public class EventCtl : MonoBehaviour
{
    public event Action PointerClickEvent;
    public void OnPointerClicked()
    {
        PointerClickEvent?.Invoke();
    }

    public event Action PointerEnterEvent;
    public void OnPointerEnter()
    {
        PointerEnterEvent?.Invoke();
    }

    public event Action PointerExitEvent;
    public void OnPointerExit()
    {
        PointerExitEvent?.Invoke();
    }
}
