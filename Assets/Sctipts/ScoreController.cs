using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText = null;
    private int oldScore = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if(GameManager.instance != null)
        {
            scoreText.text = "Score " + GameManager.instance.score;
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
        if (oldScore != GameManager.instance.score)
        {
            scoreText.text = "Score " + GameManager.instance.score;
            oldScore = GameManager.instance.score;
        }
    }
}
