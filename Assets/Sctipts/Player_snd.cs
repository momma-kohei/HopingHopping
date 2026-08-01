using UnityEngine;
using System;

public class Player_snd : MonoBehaviour
{
    [SerializeField] private Input_ctl _input;
    [SerializeField] private AudioClip _chargeSnd;
    [SerializeField] private AudioClip _launchSnd;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        NullCheck();
        _input.BtnDownAction += _audioSource.Play;
        _input.BtnUpAction += _audioSource.Stop;
        _input.BtnUpAction += OnLaunch;
    }

    void OnDisable()
    {
        _input.BtnDownAction -= _audioSource.Play;
        _input.BtnUpAction -= _audioSource.Stop;
        _input.BtnUpAction -= OnLaunch;
    }

    public void OnCharge()
    {
        if(_input.BtnHoldTime > 0)
        {
            _audioSource.Play();
        }
    }

    public void OnLaunch()
    {
        _audioSource.PlayOneShot(_launchSnd);
    }

    void NullCheck()
    {
        if (_input == null) throw new NullReferenceException("Input_ctlがアタッチされていません");
        if (_chargeSnd == null) throw new NullReferenceException("ChargeSoundがアタッチされていません");
        if (_launchSnd == null) throw new NullReferenceException("LaunchSoundがアタッチされていません");
        if (_audioSource == null) throw new NullReferenceException("AudioSourceが見つかりません");
    }
}
