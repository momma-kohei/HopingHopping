using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectCtl : MonoBehaviour
{
    [SerializeField] private Scene enterStage;
    [SerializeField] private Transform akadamaPoint;
    [SerializeField] private GameObject akadamaIcon;

    private Image img;
    private EventCtl eventCtl;

    private void Start()
    {
        img = GetComponent<Image>();
        if (img == null)
        {
            throw new Exception("画像が見つかりません．");
        }

        eventCtl = GetComponent<EventCtl>();
        if (eventCtl == null)
        {
            throw new Exception("EventControllerが見つかりません．");
        }

        eventCtl.PointerEnterEvent += OnPointerEnter;
        eventCtl.PointerExitEvent += OnPointerExit;
        eventCtl.PointerClickEvent += OnPointerClick;

        if (enterStage == Scene.S1)
        {
            SoundMgr.instance.Play(BGMkey.Select);
            OnPointerEnter();
        }
    }

    private void OnDisable()
    {
        eventCtl.PointerEnterEvent -= OnPointerEnter;
        eventCtl.PointerExitEvent -= OnPointerExit;
        eventCtl.PointerClickEvent -= OnPointerClick;
    }

    private void OnPointerClick()
    {
        SoundMgr.instance.Play(SEkey.Decide);
        GameMgr.Instance.SceneCtl.SetNextScene(enterStage);
        GameMgr.Instance.SceneCtl.TransitScene();
    }

    private void OnPointerEnter()
    {
        if (akadamaIcon.transform.position != akadamaPoint.position)
        {
            SoundMgr.instance.Play(SEkey.Hover);
            MoveAkadama();
            ShowWhite(true);
        }
    }

    private void OnPointerExit()
    {
        ShowWhite(false);
    }

    private void MoveAkadama()
    {
        akadamaIcon.transform.position = akadamaPoint.position;
    }

    private void ShowWhite(bool on)
    {
        if (on)
        {
            img.color = new Color(1f, 1f, 1f, 0.2f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
