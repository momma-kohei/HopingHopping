using UnityEngine;

public class HighScore : MonoBehaviour
{
    public void Save()
    {
        int _preBestScore = PlayerPrefs.GetInt("BestScore", 0);
        int _finalScore = GmMgr.Instance.FinalScore;

        if (_finalScore > _preBestScore)
        {
            PlayerPrefs.SetInt("BestScore", _finalScore);
        }
    }
}
