using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectCtl : MonoBehaviour
{
    [SerializeField] private SceneType enterStage;
    [SerializeField] private Transform akadamaPoint;
    [SerializeField] private GameObject akadamaIcon;

    private Image img;
    private EvntCtl evntCtl;

    private void Start()
    {
        img = GetComponent<Image>();
        if (img == null)
        {
            throw new Exception("画像が見つかりません．");
        }

        evntCtl = GetComponent<EvntCtl>();
        if (evntCtl == null)
        {
            throw new Exception("EventControllerが見つかりません．");
        }

        evntCtl.PointerEnterEvent += OnPointerEnter;
        evntCtl.PointerExitEvent += OnPointerExit;
        evntCtl.PointerClickEvent += OnPointerClick;

        if (enterStage == SceneType.ForestStage)
        {
            SoundMgr.instance.Play(BGMkey.Select);
            OnPointerEnter();
        }
    }

    private void OnDisable()
    {
        evntCtl.PointerEnterEvent -= OnPointerEnter;
        evntCtl.PointerExitEvent -= OnPointerExit;
        evntCtl.PointerClickEvent -= OnPointerClick;
    }

    private void OnPointerClick()
    {
        SoundMgr.instance.Play(SEkey.Decide);
        ScnMgr.Instance.TransitScene(enterStage);
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
