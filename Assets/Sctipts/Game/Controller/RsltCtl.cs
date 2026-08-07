using UnityEngine;
using TMPro;
using System.Collections;

public class RsltCtl : MonoBehaviour
{
    [SerializeField] private GameObject showingTexts;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject defaultTexts;
    [SerializeField] private GameObject bestScoreText;

    private void Start()
    {
        scoreText.text = GmMgr.Instance.Score.Value.ToString();
        lifeText.text = GmMgr.Instance.Life.Value.ToString();
        finalScoreText.text = GmMgr.Instance.FinalScore.ToString();

        InitUnset();

        if (GmMgr.Instance.Checker.Value < 4)
        {
            SoundMgr.instance.Play(BGMkey.GameOver);
        }
        else
        {
            SoundMgr.instance.Play(BGMkey.GameClear);
        }

        StartCoroutine(ShowFinalResult());
    }

    private IEnumerator ShowFinalResult()
    {
        yield return new WaitForSeconds(1f);
        defaultTexts.gameObject.SetActive(true);
        SoundMgr.instance.Play(SEkey.Hover); // 効果音を再生

        for (int i = 0; i < showingTexts.transform.childCount - 2; i++)
        {
            yield return new WaitForSeconds(1f); // 1秒待機
            showingTexts.transform.GetChild(i).gameObject.SetActive(true); // 子オブジェクトを順に表示する
            SoundMgr.instance.Play(SEkey.Hover); // 効果音を再生
        }
        yield return new WaitForSeconds(1.5f); // 1.5秒待機
        showingTexts.transform.GetChild(showingTexts.transform.childCount - 2).gameObject.SetActive(true);
        SoundMgr.instance.Play(SEkey.Jan);

        if (true)
        {
            yield return new WaitForSeconds(1f); // 1秒待機
            bestScoreText.SetActive(true);
            SoundMgr.instance.Play(SEkey.Jajan);
        }

        yield return new WaitForSeconds(2f); // 2秒待機
        showingTexts.transform.GetChild(showingTexts.transform.childCount - 1).gameObject.SetActive(true);
        SoundMgr.instance.Play(SEkey.Hover); // 効果音を再生
    }

    public void FinishGame()
    {
        GmMgr.Instance.Init();
        ScnMgr.Instance.TransitScene(SceneType.Title);
    }

    private void InitUnset()
    {
        defaultTexts.gameObject.SetActive(false);
        for (int i = 0; i < showingTexts.transform.childCount; i++)
        {
            showingTexts.transform.GetChild(i).gameObject.SetActive(false); // 子オブジェクトを非表示
        }
        bestScoreText.gameObject.SetActive(false);
    }
}
