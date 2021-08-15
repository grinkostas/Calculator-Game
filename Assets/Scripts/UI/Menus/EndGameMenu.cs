using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : Menu
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timeText;

    public void Show(int score, float time)
    {
        base.Show();
        _scoreText.text = score.ToString();
        float averageTime;
        if (score > 0)
        {
            averageTime = time / score;
        }
        else
        {
            averageTime = time;
        }
        _timeText.text = (System.Math.Round(averageTime, 1)).ToString() + "s";
    }
}
