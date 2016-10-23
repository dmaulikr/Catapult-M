using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TrophyShelf : MonoBehaviour {

    private Trophy[] _trophies;
    protected Trophy[] trophies {
        get {
            if(_trophies == null) {
                _trophies = GetComponentsInChildren<Trophy>();
            }
            return _trophies;
        }
        set {
            _trophies = value;
        }
    }
	public AchievementPanel achPanel;

    void OnEnable() {
        ScoreKeeper.Instance.OnReset += reset;
	}

    void OnDisable() {
        if(ScoreKeeper.Instance != null && ScoreKeeper.Instance.OnReset != null)
            ScoreKeeper.Instance.OnReset -= reset;
    }

    private void reset() {
        hideTrophies();
        sortTrophies();
    }

    private void hideTrophies() {
        foreach(Trophy t in trophies) {
            t.hide();
            t.achieved = false;
        }
    }


	void Start () {
        //hideTrophies();
        //sortTrophies();
	}

    private void sortTrophies() {
        List<Trophy> temp = new List<Trophy>(trophies);
        List<Trophy> sorted = temp.OrderBy(t => t.requiredDucks).ToList<Trophy>();
        trophies = sorted.ToArray();
    }

	public void checkAchievements() {
        check();
	}

    private void check() {
		foreach (Trophy t in trophies) {
			if (!t.achieved && t.requiredDucks <= ScoreKeeper.Instance.getScore()) {
				t.achieved = true;
                t.award();
				t.appear ();
				StartCoroutine (achieve (t));
                return;
			}
		}
    }

	private IEnumerator achieve(Trophy t) {
		achPanel.show (t);
		Time.timeScale = .01f;
		yield return new WaitForSeconds (1f * Time.timeScale);
		achPanel.hide ();
		Time.timeScale = 1f;
	}

    public int achievementLevel {
        get {
            int result = 0;
            foreach(Trophy t in trophies) {
                if (t.achieved) result++;
            }
            return result;
        }
    }

}
