using System;
using UnityEngine;
using TMPro;

public class SlctScrViw : MonoBehaviour
{
    private TextMeshProUGUI _score;

    private void Start()
    {
        _score = GetComponent<TextMeshProUGUI>();
        if ( _score == null)
        {
            throw new NullReferenceException("ScoreTextが見つかりませんでした．");
        }

        ShowScore();
    }

    private void ShowScore()
    {
        _score.text = GmMgr.Instance.Score.Value.ToString();
    }
}
