using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_mgr : MonoBehaviour
{
    [SerializeField] private int _stageNum;

    [SerializeField] private GameObject _playerIcon;
    [SerializeField] private Transform _point;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioSource _audioSource;
    //[SerializeField] private SBlind_ctl _blindOut;

    private String[] _stageSceneList = {"Stage1", "Stage2", "Stage3", "Stage4"};
    

    public event Action StageEnterEvent;
    public event Action StageClickEvent;

    void Start()
    {
        NullCheck();
        if (_stageNum == 0) MoveIcon();
        SetEnterEvent(true);
        SetClickEvent(true);
    }

    void OnDisable()
    {
        SetEnterEvent(false);
        SetClickEvent(false);
    }

    public void OnStageEnter()
    {
        if (_playerIcon.transform.position != new Vector3(_point.position.x, _point.position.y, -6))
        {
            StageEnterEvent.Invoke();
        }
    }

    public void OnStageClicked()
    {
        StageClickEvent.Invoke();
    }

    void SetEnterEvent(bool set)
    {
        if (set)
        {
            StageEnterEvent += MoveIcon;
            StageEnterEvent += PlayEnter;
        }
        else
        {
            StageEnterEvent -= MoveIcon;
            StageEnterEvent -= PlayEnter;
        }
    }

    void SetClickEvent(bool set)
    {
        if (set)
        {
            StageClickEvent += EnterStage;
        }
        else
        {
            StageClickEvent -= EnterStage;
        }
    }

    void MoveIcon()
    {
        _playerIcon.transform.position = new Vector3(_point.position.x, _point.position.y, -6);
    }

    void PlayEnter()
    {
        _audioSource.Play();
    }

    void EnterStage()
    {
        _audioSource.PlayOneShot(_clickSound);
        //_blindOut.SetIsStarted();
        Invoke(nameof(LoadStageScene), 1f);
    }

    void LoadStageScene()
    {
        if (_stageNum == 0)
        SceneManager.LoadScene(_stageSceneList[_stageNum]);
    }

    void NullCheck()
    {
        if (_stageNum < 0 || _stageNum > 4) throw new NullReferenceException("StageNumberが不正です");
        if (_playerIcon == null) throw new NullReferenceException("PlayerIconがアタッチされていません");
        if (_point == null) throw new NullReferenceException("StageListの要素がアタッチされていません");
        if (_clickSound == null) throw new NullReferenceException("ClickSoundがアタッチされていません");
        if (_audioSource == null) throw new NullReferenceException("AudioSourceがアタッチされていません");
    }
}
