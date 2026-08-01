using System;
using UnityEngine;

public class TriggerCtl : MonoBehaviour
{
    [SerializeField] CollisionTag collisionTag;
    private bool _isActive = false;

    public bool IsActive { get { return _isActive; } }
    public event Action TriggerEnterAction;
    public event Action TriggerExitAction;

    public void ResetActivity()
    {
        _isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == collisionTag.ToString())
        {
            _isActive = true;
            TriggerEnterAction?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == collisionTag.ToString())
        {
            _isActive = false;
            TriggerExitAction?.Invoke();
        }
    }
}

public enum CollisionTag
{
    Ground,
}
