using System;
using UnityEngine;

public class Trigger_ctl : MonoBehaviour
{
    [Header("接触タグ")]
    [SerializeField] private string _tag;
    private bool _flag = false;
    public bool Flag => _flag;

    public Action OnEnter;
    public Action OnExit;
    public void ResetFlag()
    {
        _flag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _tag)
        {
            _flag = true;
            OnEnter?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _tag)
        {
            _flag = false;
            OnExit?.Invoke();
        }
    }
}
