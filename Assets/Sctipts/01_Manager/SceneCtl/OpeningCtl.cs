using System;
using UnityEngine;
using UnityEngine.UI;

public class OpeningCtl : MonoBehaviour
{
    [Header("OPの設定")]
    [SerializeField] private ScreenType screenType;
    [SerializeField] private bool opActive = false;
    [SerializeField] private float OP_TIME = 1.0f;
    [SerializeField] private int OP_DELAY = 30;

    [Header("OPに使用する画像")]
    [SerializeField] private Image fade = null;
    [SerializeField] private Image slideL = null;
    [SerializeField] private Image slideR = null;

    private int _delayFrame = 0; // op開始前のフレームを数える
    private float _opTime = 0; // opの経過時間を数える
    private Image _img = null;

    private void Start()
    {
        _img = GetImage();
        _img.gameObject.SetActive(true);
        if (opActive)
        {
            OpStart();
        }
        else
        {
            OpFinish();
        }
    }

    private void Update()
    {
        if (_delayFrame > OP_DELAY)
        {
            OpUpdate(); // シーン読み込みから少し待ってUpdate処理を開始
        }
        _delayFrame++;
    }

    private void OpStart()
    {
        _opTime = 0f; // OP時間を初期化
        switch (screenType)
        {
            case ScreenType.Fade:
                _img.color = new Color(0f, 0f, 0f, 1f); // スクリーンを黒に設定
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                _img.fillAmount = 1f; // フィル量を最大に設定
                break;
        }
        _img.raycastTarget = true; // 画像の判定をオン
    }

    private void OpUpdate()
    {
        if (opActive && _opTime < OP_TIME)
        {
            switch (screenType)
            {
                case ScreenType.Fade:
                    _img.color = new Color(0f, 0f, 0f, 1f - _opTime / OP_TIME);
                    break;
                case ScreenType.SlideL:
                case ScreenType.SlideR:
                case ScreenType.Rotate:
                    _img.fillAmount = 1f - _opTime / OP_TIME;
                    break;
            }
        }
        else
        {
            OpFinish();
        }
        _opTime += Time.deltaTime;
    }

    private void OpFinish()
    {
        switch (screenType)
        {
            case ScreenType.Fade:
                _img.color = new Color(0f, 0f, 0f, 0f);
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                _img.fillAmount = 0f;
                break;
        }
        _img.raycastTarget = false; // 画像の判定をオフ
    }

    private Image GetImage()
    {
        switch (screenType)
        {
            case ScreenType.Fade:
            case ScreenType.Rotate:
                return fade;
            case ScreenType.SlideL:
                return slideL;
            case ScreenType.SlideR:
               return slideR;
            default:
                throw new NullReferenceException("スクリーンタイプの設定が不正です．");
        }
    }
}
