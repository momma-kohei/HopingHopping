using TMPro;
using UnityEngine;

public class StageNumController : MonoBehaviour
{
    private TextMeshProUGUI scoreText = null;
    private int oldScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (GameManager.instance != null)
        {
            scoreText.text = "Stage " + GameManager.instance.stageNum;
        }
        else
        {
            Debug.Log("ゲームマネージャーが見つかりませんでした．");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldScore != GameManager.instance.stageNum)
        {
            scoreText.text = "Stage " + GameManager.instance.stageNum;
            oldScore = GameManager.instance.stageNum;
        }
    }
}
