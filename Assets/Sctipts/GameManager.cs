using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int score;
    public int stageNum;
    public int continueNum;
    public int life;

    // インスタンス化直後に呼ばれる
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
