using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    #region// SoundManager自体に関する処理
    public static SoundMgr instance;
    private void Singleton() // シングルトン化を行う処理
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region// Soundデータに関する処理
    [SerializeField] private List<AudioClip> _seClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _bgmClips = new List<AudioClip>();
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();
    
    private  enum ASkey
    {
        seSource,
        bgmSource
    }
    private AudioSource GetAS(SEkey seKey)
    {
        AudioSource asc = _audioSources[(int)ASkey.seSource];
        asc.clip = _seClips[(int)seKey];
        return asc;
    }
    private AudioSource GetAS(BGMkey bgmKey)
    {
        AudioSource asc = _audioSources[(int)ASkey.bgmSource];
        asc.clip = _bgmClips[(int)bgmKey];
        return asc;
    }
    public void Play(SEkey seKey)
    {
        AudioSource asc = GetAS(seKey);
        asc.Play();
    }
    public void Stop(SEkey seKey)
    {
        AudioSource asc = GetAS(seKey);
        asc.Stop();
    }
    public void Play(BGMkey bgmKey)
    {
        AudioSource asc = GetAS(bgmKey);
        asc.loop = true;
        asc.Play();
    }
    public void Stop(BGMkey bgmKey)
    {
        AudioSource asc = GetAS(bgmKey);
        asc.Stop();
    }
    public void PauseBGM(BGMkey bgmKey)
    {
        AudioSource asc = GetAS(bgmKey);
        asc.Pause();
    }
    public void UnPauseBGM(BGMkey bgmKey)
    {
        AudioSource asc = GetAS(bgmKey);
        asc.UnPause();
    }
    #endregion

    private void Awake()
    {
        Singleton();
    }
}

public enum SEkey
{
    Decide,
    Hover,
    Charge,
    Launch
}
public enum BGMkey
{
    Title,
    Select,
    Stage1
}
