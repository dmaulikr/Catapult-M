using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ScoreKeeper :  Singleton<ScoreKeeper> {

    [SerializeField]
    private TrophyShelf trophyShelf;
    [SerializeField]
    private LosePanel losePanel;
    private bool canLose = false;

    public delegate void Reset();
    public Reset OnReset;

    public Text text;
	protected ScoreKeeper() {}

    public float ammoBonusMultiplier = 1f;

	public void Awake() {
        losePanel.hide();
		Duck.OnGotAHit += registerAPoint;
        Duck.OnNeverGotHit += applyPenalty;
        reset();
    }

    public void Destroy() {
		Duck.OnGotAHit -= registerAPoint;
        Duck.OnNeverGotHit -= applyPenalty;
	}

    [SerializeField]
    private PercentageBar healthBar;
    [SerializeField]
    private int maxHealth = 50;
    private int _health;
    private float healthPercent { get { return (float)_health / (float)maxHealth; } }
    private int health {
        get { return _health; }
        set {
            _health = value;
            healthBar.set(healthPercent);
            if (_health < 0) {
                lose("You died");
            }
        }
    }

    //TODO: missed ducks no longer subtract from score. instead they subtract from lives
	private int _score;
    private int score {
        get { return _score; }
        set {
            AmmoClip.Instance.addAmmo(getAmmoBonus(value - _score));
            _score = value;
            HighScore.Instance.updateHighscore(_score);
            if (text != null)
                text.text = "" + _score;
            trophyShelf.checkAchievements();
            if (!canLose) {
                canLose = _score > 0;
            } else if (_score < 0) {
                lose("Score became negative");
            }
        }
    }

    private int getAmmoBonus(int value) {
        return (int)Mathf.Round(value * ammoBonusMultiplier);
    }

    public int getScore() {
		return score;
	}

	public void registerAPoint(Boulder boulder) {
		score += boulder.preciousness;
/*
        health += Mathf.RoundToInt(Mathf.Clamp((float)boulder.preciousness / 2f, 1f, 5f));
        health = health < maxHealth? health : maxHealth;
*/
	}

    private void applyPenalty(Duck duck) {
        AmmoClip.Instance.addAmmo(-1);
        //health -= duck.missPenalty;
    }

	public void lose(string because) {
        StartCoroutine(showAndLose(because));
	}

    private IEnumerator showAndLose(string because) {
        losePanel.show(because);
        yield return new WaitForSeconds(4f);
        losePanel.hide();
        reset();
        OnReset();
    }

    private void reset() {
        canLose = false;
        _score = 0;
        _health = maxHealth;
        text.text = "";
    }
}
