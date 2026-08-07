using System;
using UnityEngine;
using UnityEngine.UI;

public class OPCtl : MonoBehaviour
{
    [Header("OPの設定")]
    [SerializeField] private ScreenType screenType; // OPの種類
    [SerializeField] private bool opActive = false; // OPを実行するかどうか
    [SerializeField] private float OP_TIME = 1.0f; // OPの実行時間
    [SerializeField] private int OP_DELAY = 30;
    [SerializeField] private Image screen = null; // OPに使用する画像

    [SerializeField] private InputCtl inputCtl; // プレイヤーの操作を制御するためのInputCtlへの参照

    private int _delayFrame = 0; // op開始前のフレームを数える
    private float _opTime = 0; // opの経過時間を数える
    private bool _opFinished = false; // OPが終了したかどうかを表すフラグ

    private void Start()
    {
        if (screen == null)
        {
            throw new NullReferenceException("OPに使用する画像が設定されていません．");
        }
        SetScreenType(screenType); // OPの種類に応じて画像の設定を行う

        if (opActive)
        {
            OPStart(); // OPを実行する場合は開始する
        }
        else
        {
            OPFinish(); // OPを実行しない場合は終了させておく
        }
    }

    private void Update()
    {
        if (_delayFrame > OP_DELAY)
        {
            OPUpdate(); // ロード直後の都合上シーン読み込みから少し待ってUpdate処理を開始
        }
        _delayFrame++;
    }

    /// <summary>
    /// OPを開始するメソッド
    /// </summary>
    public void OPStart()
    {
        if (inputCtl != null)
        {
            inputCtl.SetPlayerActions(false);
        }
        screen.gameObject.SetActive(true); // 画像を有効化
        _opTime = 0f; // OP時間を初期化
        switch (screenType)
        {
            case ScreenType.Fade:
                screen.color = new Color(0f, 0f, 0f, 1f); // スクリーンを黒から開始する
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                screen.fillAmount = 1f; // フィル量を1から開始する
                break;
        }
        screen.raycastTarget = true; // 画像の当たり判定をオン
    }

    /// <summary>
    /// 毎秒のOP更新処理を行うメソッド
    /// </summary>
    private void OPUpdate()
    {
        if (opActive && _opTime < OP_TIME)
        {
            switch (screenType)
            {
                case ScreenType.Fade:
                    screen.color = new Color(0f, 0f, 0f, 1f - _opTime / OP_TIME);
                    break;
                case ScreenType.SlideL:
                case ScreenType.SlideR:
                case ScreenType.Rotate:
                    screen.fillAmount = 1f - _opTime / OP_TIME;
                    break;
            }
        }
        else if (!_opFinished)
        {
            OPFinish();
        }
        _opTime += Time.deltaTime;
    }

    /// <summary>
    /// OPの終了処理を行うメソッド
    /// </summary>
    private void OPFinish()
    {
        switch (screenType)
        {
            case ScreenType.Fade:
                screen.color = new Color(0f, 0f, 0f, 0f); // スクリーンを透明で終了する
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                screen.fillAmount = 0f; // フィル量を0で終了する
                break;
        }
        screen.raycastTarget = false; // 画像の判定をオフ
        screen.gameObject.SetActive(false); // 画像を無効化する
        _opFinished = true; // OP終了フラグを立てる
        if (inputCtl != null)
        {
            inputCtl.SetPlayerActions(true); // プレイヤーの操作を有効化
        }
    }

    /// <summary>
    /// OPの種類に応じて画像の設定を行うメソッド
    /// </summary>
    /// <param name="type"></param>
    private void SetScreenType(ScreenType type)
    {
        switch (type)
        {
            case ScreenType.Fade:
            case ScreenType.SlideL:
                screen.fillMethod = Image.FillMethod.Horizontal;
                screen.fillOrigin = 0;
                break;
            case ScreenType.SlideR:
                screen.fillMethod = Image.FillMethod.Horizontal;
                screen.fillOrigin = 1;
                break;
            case ScreenType.Rotate:
                screen.fillMethod = Image.FillMethod.Radial360;
                screen.fillOrigin = 0;
                break;
        }
    }
}

public enum ScreenType
{
    Fade,
    SlideL,
    SlideR,
    Rotate,
}
