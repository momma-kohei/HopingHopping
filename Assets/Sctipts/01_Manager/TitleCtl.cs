using System;
using UnityEngine;

public class TitleCtl : MonoBehaviour
{
    [SerializeField] private EventCtl eventCtl;
    [SerializeField] private ScnMgr scnMgr;

    private void Start()
    {
        if (eventCtl == null)
        {
            throw new Exception("EventManagerがアタッチされていません．");
        }

        eventCtl.PointerClickEvent += OnPointerClick;
        SoundMgr.instance.Play(BGMkey.Title);
    }

    private void OnPointerClick()
    {
        SoundMgr.instance.Stop(BGMkey.Title);
        SoundMgr.instance.Play(SEkey.Decide);
        scnMgr.TransitScene(SceneType.StageSelect);
    }
}
