using System;
using UnityEngine;
using UnityEngine.UI;

public class EDCtl : MonoBehaviour
{
    [Header("EDの設定")]
    [SerializeField] private ScreenType screenType; // EDの種類
    [SerializeField] private bool edActive = false; // EDを実行するかどうか
    [SerializeField] private float ED_TIME = 1.0f; // EDの実行時間
    [SerializeField] private Image screen = null; // EDに使用する画像

    private bool _edDoing = false; // ed実行中を表すフラグ
    private float _edTime = 0; // edの経過時間を数える

    /// <summary>
    /// EDの実行時間を猶予付きで取得するプロパティ
    /// </summary>
    public float EDTime { get { return ED_TIME + 0.2f; } }

    private void Start()
    {
        if (screen == null)
        {
            throw new NullReferenceException("EDに使用する画像が設定されていません．");
        }
        if (!edActive)
        {
            EDFinish(); // EDを実行しない場合は終了させておく
        }
    }

    private void Update()
    {
        if (edActive && _edDoing) // EDが有効でかつ実行中の場合は更新処理を行う
        {
            EDUpdate();
        }
    }

    /// <summary>
    /// EDを開始するメソッド
    /// </summary>
    public void EDStart()
    {
        SetScreenType(screenType); // EDの種類に応じて画像の設定を行う
        screen.gameObject.SetActive(true); // 画像を有効化
        _edTime = 0f; // OP時間を初期化
        screen.color = new Color(0f, 0f, 0f, 1f); // スクリーンを黒で初期化
        screen.fillAmount = 1f; // フィル量を1で初期化
        switch (screenType)
        {
            case ScreenType.Fade:
                screen.color = new Color(0f, 0f, 0f, 0f); // スクリーンを透明から開始する
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                screen.fillAmount = 0f; // フィル量を0から開始する
                break;
        }
        screen.raycastTarget = true; // 画像の当たり判定をオン
        _edDoing = true; // ED実行中フラグをオン
    }

    /// <summary>
    /// 毎秒のED更新処理を行うメソッド
    /// </summary>
    private void EDUpdate()
    {
        if (edActive && _edTime < ED_TIME)
        {
            switch (screenType)
            {
                case ScreenType.Fade:
                    screen.color = new Color(0f, 0f, 0f, _edTime / ED_TIME);
                    break;
                case ScreenType.SlideL:
                case ScreenType.SlideR:
                case ScreenType.Rotate:
                    screen.fillAmount = _edTime / ED_TIME;
                    break;
            }
        }
        else
        {
            EDFinish();
        }
        _edTime += Time.deltaTime;
    }

    /// <summary>
    /// EDの終了処理を行うメソッド
    /// </summary>
    private void EDFinish()
    {
        _edDoing = false;
        switch (screenType)
        {
            case ScreenType.Fade:
                screen.color = new Color(0f, 0f, 0f, 1f); // スクリーンを黒で終了する
                break;
            case ScreenType.SlideL:
            case ScreenType.SlideR:
            case ScreenType.Rotate:
                screen.fillAmount = 1f; // フィル量を1で終了する
                break;
        }
    }

    /// <summary>
    /// EDの種類に応じて画像の設定を行うメソッド
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
