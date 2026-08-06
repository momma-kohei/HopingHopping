using UnityEngine;

public class HighScore : MonoBehaviour
{
    private int _score = 0;
    private int _checker = 0;
    private int _life = 0;

    public void SetValue()
    {
        _score = GmMgr.Instance.Score.Value;
        _checker = GmMgr.Instance.Checker.Value;
        _life = GmMgr.Instance.Life.Value;
    }

    public void SavePreScore()
    {
        PlayerPrefs.SetInt("PreScore", _score);
        PlayerPrefs.SetInt("PreChecker", _checker);
        PlayerPrefs.SetInt("PreLife", _life);
    }

    public void SaveHighScore()
    {
        int _preBestScore = PlayerPrefs.GetInt("BestScore", 0);
        int _preBestChecker = PlayerPrefs.GetInt("BestChecker", 0);
        int _preBestLife = PlayerPrefs.GetInt("BestLife", 0);

        if (CalculateRecord(_score, _checker, _life) > CalculateRecord(_preBestScore, _preBestChecker, _preBestLife))
        {
            PlayerPrefs.SetInt("BestScore", _score);
            PlayerPrefs.SetInt("BestChecker", _checker);
            PlayerPrefs.SetInt("BestLife", _life);
        }
    }

    private int CalculateRecord(int score, int checker, int life)
    {
        int _record = score + (checker * 100) + (life * 50);
        return _record;
    }
}
