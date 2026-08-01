using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class TitleCtl : MonoBehaviour
{
    [SerializeField] private EventCtl eventCtl;

    private void Start()
    {
        if (eventCtl == null)
        {
            throw new Exception("EventManagerがアタッチされていません．");
        }

    eventCtl.PointerClickEvent += OnPointerClick;
        GameMgr.Instance.SceneCtl.SetNextScene(Scene.Select);
        SoundMgr.instance.Play(BGMkey.Title);
    }

    private void OnPointerClick()
    {
        SoundMgr.instance.Stop(BGMkey.Title);
        SoundMgr.instance.Play(SEkey.Decide);
        GameMgr.Instance.SceneCtl.TransitScene();
    }
}
