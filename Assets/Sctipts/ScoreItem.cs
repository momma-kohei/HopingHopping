using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [Header("加算するスコア")] public int addScore;
    [Header("プレイヤー判定")] public PlayerTrigerCheck playerCheck;

    // Update is called once per frame
    void Update()
    {
        if (playerCheck.isOn)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.score += addScore;
                Destroy(this.gameObject);
            }
        }
    }
}
