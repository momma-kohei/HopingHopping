using System;
using UnityEngine;
using UnityEngine.UI;

public class EndingCtl : MonoBehaviour
{
    [Header("EDの設定")]
    [SerializeField] private ScreenType screenType;
    [SerializeField] private bool edActive = false;
    [SerializeField] private float ED_TIME = 1.0f;

    [Header("EDに使用する画像")]
    [SerializeField] private Image fade = null;
    [SerializeField] private Image slideL = null;
    [SerializeField] private Image slideR = null;

    private bool _edDoing = false; // edの実行を行うフラグ
    private float _edTime = 0; // edの経過時間を数える
    private Image _img = null;

    public float EdTime => ED_TIME;

    private void Start()
    {
        _img = GetImage();
        if (!edActive)
        {
            EdFinish();
        }
    }

    private void Update()
    {
        if (edActive && _edDoing)
        {
            EdUpdate();
        }
    }

    public void EdStart()
    {
        _img.gameObject.SetActive(true);
        _edTime = 0f; // OP時間を初期化
        switch (screenType)
        {
            case ScreenType.Fade:
                _img.color = new Color(0f, 0f, 0f, 0f); // スクリーンを透明に設定
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                _img.fillAmount = 0f; // フィル量を0に設定
                break;
        }
        _img.raycastTarget = true; // 画像の判定をオン
        _edDoing= true;
    }

    private void EdUpdate()
    {
        if (_edTime < ED_TIME)
        {
            switch (screenType)
            {
                case ScreenType.Fade:
                    _img.color = new Color(0f, 0f, 0f,_edTime / ED_TIME);
                    break;
                case ScreenType.SlideL:
                case ScreenType.SlideR:
                case ScreenType.Rotate:
                    _img.fillAmount = _edTime / ED_TIME;
                    break;
            }
        }
        else
        {
            EdFinish();
        }
        _edTime += Time.deltaTime;
    }

    private void EdFinish()
    {
        _edDoing = false;
        switch (screenType)
        {
            case ScreenType.Fade:
                _img.color = new Color(0f, 0f, 0f, 1f);
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                _img.fillAmount = 1f;
                break;
        }
        _img.gameObject.SetActive(false);
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
