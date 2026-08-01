using System;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{
    [SerializeField] private GameObject akadamaIcon;
    private Transform akadamaPoint = null;
    private Image img = null;

    private void Awake()
    {
        img = GetComponent<Image>();
        if (img == null)
        {
            throw new NullReferenceException("パネル画像が見つかりません．");
        }
        akadamaPoint = transform.GetChild(0).transform;
        if (akadamaPoint == null)
        {
            throw new NullReferenceException("AkadamaPointが見つかりません．");
        }
    }

    public void MoveAkadama()
    {
         akadamaIcon.transform.position = akadamaPoint.position;
    }

    public void Hover(bool on)
    {
        if (on)
        {
            SoundMgr.instance.Play(SEkey.Hover);
            MoveAkadama();
            img.color = new Color(1f, 1f, 1f, 0.2f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
