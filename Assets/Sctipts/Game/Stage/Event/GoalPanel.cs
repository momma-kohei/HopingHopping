using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoalPanel : MonoBehaviour
{
    [SerializeField] private GameObject goalPanel;
    [SerializeField] private TextMeshProUGUI scoreResult;

    /// <summary>
    /// ゴール時の結果表示を行うコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowResult()
    {
        scoreResult.text = "TotalScore: " + GmMgr.Instance.Score.Value.ToString(); // スコアを反映
        SoundMgr.instance.Stop(BGMkey.Stage1); // ステージBGMを停止

        goalPanel.SetActive(true); // ゴールパネルを表示
        goalPanel.transform.GetChild(0).gameObject.SetActive(true);
        for ( int i = 1; i < goalPanel.transform.childCount - 1; i++)
        {
            goalPanel.transform.GetChild(i).gameObject.SetActive(true); // 子オブジェクトも表示する
            SoundMgr.instance.Play(SEkey.Hover); // 効果音を再生
            yield return new WaitForSeconds(1f); // 1秒待機
        }
        goalPanel.transform.GetChild(goalPanel.transform.childCount - 1).gameObject.SetActive(true); // 子オブジェクトも表示する
        SoundMgr.instance.Play(SEkey.Clear); // クリア音を再生
    }  
}
