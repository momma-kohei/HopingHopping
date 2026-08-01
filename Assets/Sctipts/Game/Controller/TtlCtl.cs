using UnityEngine;

public class TtlCtl : MonoBehaviour // タイトルシーンの制御を行うクラス
{
    [SerializeField] private EvntCtl evntCtl;

    private void Start()
    {
        if (evntCtl == null)
        {
            throw new System.Exception("EventManagerがアタッチされていません．");
        }

        evntCtl.PointerClickEvent += FInishTitleScene;
        SoundMgr.instance.Play(BGMkey.Title); // BGMを再生する
    }

    private void FInishTitleScene()
    {
        SoundMgr.instance.Stop(BGMkey.Title); // BGMを止める
        SoundMgr.instance.Play(SEkey.Decide); // 決定音を再生する
        ScnMgr.Instance.TransitScene(SceneType.StageSelect); // ステージ選択シーンに遷移する
    }
}
