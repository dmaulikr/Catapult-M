using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public static class PlayerKeys
{
    public static string HighScore = "HIGH_SCORE";
    public static string LaunchedGameCount = "LAUNCH_GAME_COUNT";
    public static string UnPauses = "UNPAUSES";
    internal static string PlayerName;
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
            StartCoroutine(ServerConnection.Instance.SubmitScore());
        }
    }
}
