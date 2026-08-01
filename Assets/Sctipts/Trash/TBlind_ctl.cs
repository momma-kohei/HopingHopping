using UnityEngine;
using UnityEngine.UI;
using System;

public class TBlind_ctl : MonoBehaviour
{
    private Image _blind;
    private Color _color;
    private bool _isStarted = false;
    private float _time = 0;

    void Start()
    {
        _isStarted = false;
        _blind = GetComponent<Image>();
        if (_blind == null) throw new NullReferenceException("画像が見つかりません");
        _color = _blind.color;
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_isStarted)
        {
            _time += Time.deltaTime;
            FadeOut();
            SetColor();
        }
    }

    public void OnStartClicked()
    {
        Debug.Log("OnStartCliced");
        this.gameObject.SetActive(true);
        _isStarted = true;
    }

    void SetColor()
    {
        _blind.color = _color;
    }
    
    void FadeOut()
    {
        _color.a = _time;
    }
}
