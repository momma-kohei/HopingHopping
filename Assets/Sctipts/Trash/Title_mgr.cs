using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_mgr : MonoBehaviour
{
    [SerializeField] private TBlind_ctl _blindCtl;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnTitleClicked()
    {
        _blindCtl.OnStartClicked();
        _audioSource.Play();
        Invoke(nameof(LoadSelectScene), 1f);
    }

    void LoadSelectScene()
    {
        SceneManager.LoadScene("Select");
    }
}
