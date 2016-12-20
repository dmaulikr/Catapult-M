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
    //private bool canLose = true;

    public delegate void Reset();
    public Reset OnReset;

    public Text text;
	protected ScoreKeeper() {}

    public float ammoBonusMultiplier = 1f;

    private HitsInARow hitsInARow;

	public void Awake() {
        hitsInARow = GetComponent<HitsInARow>();
		Duck.OnDuckGotHit += registerAPoint;
        Duck.OnNeverGotHit += applyPenalty;
        reset();
    }

    public void Destroy() {
		Duck.OnDuckGotHit -= registerAPoint;
        Duck.OnNeverGotHit -= applyPenalty;
        
    }

    [SerializeField]
    private PercentageBar healthBar;
    [SerializeField]
    private int maxHealth = 1;
    private int _health;
    private float healthPercent { get { return (float)_health / (float)maxHealth; } }
    private int health {
        get { return _health; }
        set {
            if(value > _health) {
                healthBar.glow();
            }
            _health = value;
            healthBar.set(healthPercent);
            healthBar.setNumbers(_health, maxHealth);
            if (_health < 0) {
                lose("You died");
            }
        }
    }

    //TODO: missed ducks no longer subtract from score. instead they subtract from lives
	private int _score;
    [SerializeField]
    private int hitsInARowBonus;
    private bool deathProcessStarted;

    private int score {
        get { return _score; }
        set {
            
            _score = value;
            HighScore.Instance.updateHighscore(_score);
            if (text != null)
                text.text = "" + _score;
            trophyShelf.checkAchievements();
            //if (!canLose) {
            //    canLose = _score > 0;
            //} 
            //else if (_score < 0) {
            //    lose("Score became negative");
            //}
        }
    }

    private int _boulderAirballCount;
    public int airballCount { get { return _boulderAirballCount; } }
    public void addAirball() {
        _boulderAirballCount++;
    }
    public void decreaseAirballCount() {
        _boulderAirballCount = Mathf.FloorToInt(_boulderAirballCount / 2);
    }
    public void resetAirballCount() {
        _boulderAirballCount = 0;
    }

    private int getAmmoBonus(int value) {
        return (int)Mathf.Round(value * ammoBonusMultiplier);
    }

    public int getScore() {
		return score;
	}

	public void registerAPoint(DuckHitInfo dhi) {
        score += dhi.boulder.preciousness;
        hitsInARow.addHit(dhi, () => {
            giveBonusAmmo(dhi);
        });
	}

    private void giveHealth(DuckHitInfo dhi) {
        giveHealth(Mathf.RoundToInt(Mathf.Clamp(dhi.boulder.preciousness / 2f, 1f, 5f)));
    }

    private void giveHealth(int amt) {
        health = Mathf.Clamp(health + amt, 0, maxHealth);
    }

    private void giveBonusAmmo(DuckHitInfo dhi) {
        int overage = AmmoClip.Instance.addAmmo(getAmmoBonus(Mathf.Max(1, Mathf.RoundToInt(dhi.duck.evilness * 4f)) + 15));
        giveHealth(overage);
    }

    private void applyPenalty(Duck duck) {
        hitsInARow.resetHits();
        AmmoClip.Instance.addAmmo(Mathf.RoundToInt(-duck.missPenalty));
        health -= duck.missPenalty  * Mathf.RoundToInt(1 + Mathf.Clamp(airballCount / 2f, 0f, 2f));
    }

	public void lose(string because) {
        if(deathProcessStarted) { return; }
        StartCoroutine(ServerConnection.Instance.SubmitScore());
        deathProcessStarted = true;
        StartCoroutine(showAndLose(because));
	}

    private IEnumerator showAndLose(string because) {
        losePanel.show(because);
        yield return new WaitForSeconds(4f);
        losePanel.hide();
        reset();
        OnReset();
        deathProcessStarted = false;
    }

    private void reset() {
        losePanel.hide();
        hitsInARow.resetHits();
        _score = 0;
        health = maxHealth;
        AmmoClip.Instance.resetAmmo();
        text.text = "";
        resetAirballCount();
        OnReset();
    }
}
