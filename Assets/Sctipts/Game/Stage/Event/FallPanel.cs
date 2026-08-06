using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallPanel : MonoBehaviour
{
    [SerializeField] private GameObject fallPanel;
    [SerializeField] private TextMeshProUGUI lifeBefore;
    [SerializeField] private TextMeshProUGUI lifeAfter;
    [SerializeField] private InputCtl inputCtl;

    public IEnumerator ShowRetry()
    {
        lifeBefore.text = "× " + (GmMgr.Instance.Life.Value + 1).ToString(); // 今のライフを反映
        lifeAfter.text = (GmMgr.Instance.Life.Value).ToString(); // 変更後ライフを反映

        SoundMgr.instance.PauseBGM((BGMkey)SceneManager.GetActiveScene().buildIndex); // BGMを一時停止

        fallPanel.SetActive(true); // フォールパネルを表示
        fallPanel.transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < fallPanel.transform.childCount; i++)
        {
            fallPanel.transform.GetChild(i).gameObject.SetActive(true); // 子オブジェクトも表示する
            //SoundMgr.instance.Play(SEkey.Hover); // 効果音を再生
            yield return new WaitForSeconds(1f); // 1秒待機
        }
    }

    public void Retry()
    {
        SoundMgr.instance.Play(SEkey.Decide); // 決定音を再生
        for (int i = 0; i < fallPanel.transform.childCount; i++)
        {
            fallPanel.transform.GetChild(i).gameObject.SetActive(false); // 子オブジェクトも非表示
        }
        fallPanel.SetActive(false); // フォールパネルを非表示
        StgCtl.Instance.Respawn(); // プレイヤーをリスポーンさせる
        SoundMgr.instance.UnPauseBGM((BGMkey)SceneManager.GetActiveScene().buildIndex); // BGMを再開
        inputCtl.SetPlayerActions(true); // プレイヤーの操作を有効化
    }
}
