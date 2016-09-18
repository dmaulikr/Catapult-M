using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public static class PlayerKeys
{
    public static string HighScore = "HIGH_SCORE";
    
}


public class HighScore : Singleton<HighScore> {

    protected HighScore() { }

    private int highScore;
    public Text highScoreText;

    public void Awake() {
        if (PlayerPrefs.HasKey(PlayerKeys.HighScore)) {
            updateHighscore(PlayerPrefs.GetInt(PlayerKeys.HighScore));
        }
    }

    public void updateHighscore(int score) {
        if (score > highScore) {
            highScore = score;
            highScoreText.text = "" + highScore;
            PlayerPrefs.SetInt(PlayerKeys.HighScore, highScore);
        }
    }
}
