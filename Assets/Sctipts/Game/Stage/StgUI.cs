using UnityEngine;
using TMPro;

public class StgUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI lifeText = null;
    [SerializeField] private int stageNumber = 1; // ステージ番号を設定する変数

    private void Start()
    {
        InitUIValues();
    }

    private void Update()
    {
        UpdateScore();
        UpdateLife();
    }

    public void InitUIValues()
    {
        stageText.text = "Stage " + stageNumber;
        scoreText.text = GmMgr.Instance.Score.Value.ToString();
        lifeText.text = "× " + GmMgr.Instance.Life.Value;
    }

    private int _preScore; // スコアの前回の値を保持する変数
    private void UpdateScore()
    {
        if (_preScore == GmMgr.Instance.Score.Value) return; // スコアが変わっていない場合は更新しない
        scoreText.text = GmMgr.Instance.Score.Value.ToString();
        _preScore = GmMgr.Instance.Score.Value;
    }

    private int _preLife; // ライフの前回の値を保持する変数
    private void UpdateLife()
    {
        if (_preLife == GmMgr.Instance.Life.Value) return; // ライフが変わっていない場合は更新しない
        lifeText.text = "× " + GmMgr.Instance.Life.Value;
        _preLife = GmMgr.Instance.Life.Value;
    }
}
