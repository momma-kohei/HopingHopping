using UnityEngine;

public class SlctCtl : MonoBehaviour
{
    [SerializeField] private GameObject stagePanels;
    [SerializeField] private GameObject akadamaIcon;

    private void Start()
    {
        SoundMgr.instance.Stop(SEkey.Charge);
        SoundMgr.instance.Play(BGMkey.Select);
        stagePanels.transform.GetChild(0).GetComponent<StagePanel>().MoveAkadama();

        for (int i = 0; i < stagePanels.transform.childCount; i++)
        {
            int index = i;
            EvntCtl _evntCtl = stagePanels.transform.GetChild(i).GetComponent<EvntCtl>();
            _evntCtl.PointerClickEvent += () => SoundMgr.instance.Stop(BGMkey.Select);
            _evntCtl.PointerClickEvent += () => SoundMgr.instance.Play(SEkey.Decide);
            _evntCtl.PointerClickEvent += () => ScnMgr.Instance.TransitScene((SceneType)(index + 2));
            StagePanel _panel = stagePanels.transform.GetChild(i).GetComponent<StagePanel>();
            _evntCtl.PointerEnterEvent += () => _panel.Hover(true);
            _evntCtl.PointerExitEvent += () => _panel.Hover(false);
        }
    }

    public void ExitStageSelect()
    {
        SoundMgr.instance.Play(SEkey.Decide); // 決定音を再生
        Time.timeScale = 1f;
        ScnMgr.Instance.TransitScene(SceneType.Title); // ステージ選択画面に遷移
    }
}
