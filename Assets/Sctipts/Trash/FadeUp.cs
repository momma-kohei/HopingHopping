using UnityEngine;
using UnityEngine.UI;

public class FadeUp : MonoBehaviour
{
    [Header("フェードイン済")] public bool fadeInfinished;

    private Image img = null;
    private int frameCount = 0;
    private float frameTime = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool finFadeIn = false;
    private bool finFadeOut = false;

    /// <summary>
    /// フェードインを開始する
    /// </summary>
    public void StartFadeIn()
    {
        if(fadeIn || fadeOut)
        {
            return;
        }
        fadeIn = true;
        finFadeIn = false;
        frameTime = 0;
        img.fillAmount = 1f;
        img.raycastTarget = true;
    }
    /// <summary>
    /// フェードインの完了を確認する
    /// </summary>
    /// <returns>フェードインが完了したか</returns>
    public bool IsFadeInFinished()
    {
        return finFadeIn;
    }
    /// <summary>
    /// フェードイアウトを開始する
    /// </summary>
    public void StartFadeOut()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeOut = true;
        finFadeOut = false;
        frameTime = 0;
        img.fillAmount = 0f;
        img.raycastTarget = true;
    }
    /// <summary>
    /// フェードアウトの完了を確認する
    /// </summary>
    /// <returns>フェードアウトが完了したか</returns>
    public bool IsFadeOutFinished()
    {
        return finFadeOut;
    }

    void Start()
    {
        img = GetComponent<Image>();

        if (fadeInfinished)
        {
            FadeInFInish();
        }
        else
        {
            StartFadeIn();
        }
    }

    void Update()
    {
        if(frameCount > 2)
        {
            if (fadeIn)
            {
                FadeInUpdate();
            }
            else if (fadeOut)
            {
                FadeOutUpdate();
            }
        }
        ++frameCount;
    }
    /// <summary>
    /// フェードインを実行する
    /// </summary>
    private void FadeInUpdate()
    {
        // フェード実行
        if (frameTime < 1f)
        {
            img.fillAmount = 1f - frameTime;
        }
        // フェード完了
        else
        {
            FadeInFInish();
        }
        frameTime += Time.deltaTime;
    }
    /// <summary>
    /// フェードインの完了処理を行う
    /// </summary>
    private void FadeInFInish()
    {
        img.fillAmount = 0f;
        img.raycastTarget = false;

        frameTime = 0f;
        fadeIn = false;
        finFadeIn = true;
    }
    /// <summary>
    /// フェードアウトを実行する
    /// </summary>
    private void FadeOutUpdate()
    {
        // フェード実行
        if (frameTime < 1f)
        {
            img.fillAmount = frameTime;
        }
        // フェード完了
        else
        {
            FadeOutFInish();
        }
        frameTime += Time.deltaTime;
    }
    /// <summary>
    /// フェードアウト完了処理を行う
    /// </summary>
    private void FadeOutFInish()
    {
        img.fillAmount = 1f;
        img.raycastTarget = false;

        frameTime = 0f;
        fadeOut = false;
        finFadeOut = true;
    }
}
