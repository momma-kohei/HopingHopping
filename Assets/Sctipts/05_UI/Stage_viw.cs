using UnityEngine;
using TMPro;

public class Stage_viw : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageText = null;
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    [SerializeField] private TextMeshProUGUI _lifeText = null;
    private Stage_ctl _stage_ctl;

    void Awake()
    {
        _stage_ctl = GetComponent<Stage_ctl>();
    }
    void Start()
    {
        CheckNull();
        ShowStageNum();
    }

    void Update()
    {
        ShowScore();
        ShowLife();
    }

    public void ShowStageNum()
    {
        _stageText.text = "Stage " + GameManager.instance.stage.GetStage();
    }

    private int _preScore;
    public void ShowScore()
    {
        if (_preScore == _stage_ctl.score.Get()) return; // スコアが変わっていない場合は更新しない
        _scoreText.text = _stage_ctl.score.Get().ToString();
        _preScore = _stage_ctl.score.Get();
    }

    private int _preLife;
    public void ShowLife()
    {
        if (_preLife == _stage_ctl.life.Get()) return; // ライフが変わっていない場合は更新しない
        _lifeText.text = "× " + _stage_ctl.life.Get();
        _preLife = _stage_ctl.life.Get();
    }

    void CheckNull()
    {
        if (_stageText == null) throw new System.NullReferenceException("_stageNumTextが設定されていません．");
        if (_scoreText == null) throw new System.NullReferenceException("_scoreTextが設定されていません．");
        if (_lifeText == null) throw new System.NullReferenceException("_lifeTextが設定されていません．");
        if (_stage_ctl == null) throw new System.NullReferenceException("Stage_ctlコンポーネントが見つかりません．");
        if (GameManager.instance == null) throw new System.NullReferenceException("ゲームマネージャーが見つかりませんでした．");
    }
}
