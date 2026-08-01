using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal_ctl : MonoBehaviour
{
    private Trigger_ctl _trigger_ctl;

    void Start()
    {
        _trigger_ctl = GetComponent<Trigger_ctl>();
        CheckNull();
        _trigger_ctl.OnEnter += Main;
    }

    private void OnDisable()
    {
        _trigger_ctl.OnEnter -= Main;
    }

    void Main()
    {
        Debug.Log("Stage Clear!");
        GameManager.instance.StageNum++;
        Invoke(nameof(LoadSelectScene), 1f);
    }

    void LoadSelectScene()
    {
        SceneManager.LoadScene("Select");
    }

    void CheckNull()
    {
        if (_trigger_ctl == null) throw new System.Exception("Trigger_ctlコンポーネントが見つかりません．");
        if (GameManager.instance == null) throw new System.Exception("GameManagerが見つかりません．");
    }
}
