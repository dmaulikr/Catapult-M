using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DuckDispenser : MonoBehaviour {

    [SerializeField]
    protected DuckOriginals originals;
    
    protected ChoiceMode choiceMode;

	public float baseFrequency = 2f;
    private float frequencyMultiplier;
    private float speedMultiplier = 1f;

    private int[] intensityMilestoneIntervals;

    private bool shouldDispense = true;
    [SerializeField]
    private int baseIntensityInterval = 2;

    private bool startedMaxFrequency;
    public float duckBarageTimeSeconds = 6f;
    public float maxFrequency = .025f;
    private bool dispensingAlready;

    public void Awake() {
        foreach (Duck d in GetComponentsInChildren<Duck>()) {
            if (d.isClone) { continue; }
            d.gameObject.SetActive(false);
        }
        choiceMode = new ChoiceMode();
        setupMilestoneIntervals();
    }
    private void setupMilestoneIntervals() {
        intensityMilestoneIntervals = new int[choiceMode.Length];
        for(int i = 0; i < choiceMode.Length; ++i) {
            intensityMilestoneIntervals[i] = (i + 1) * baseIntensityInterval + (int)Mathf.Pow((float)(baseIntensityInterval * i), 1.2f);
        }
    }

    void Start () {
        //StartCoroutine(dispense());
	}

    void OnEnable() {
		Duck.OnDuckGotHit += increaseFrequency;
        Duck.OnNeverGotHit += decreaseFrequency;
        ScoreKeeper.Instance.OnReset += reset;
	}

    void OnDisable() {
		Duck.OnDuckGotHit -= increaseFrequency;
        Duck.OnNeverGotHit -= decreaseFrequency;
        if (ScoreKeeper.Instance && ScoreKeeper.Instance.OnReset != null) {
            ScoreKeeper.Instance.OnReset -= reset;
        }
	}

    public void onGMPause(bool paused) {
        print("got gm paused is " + paused);
        if(paused) {
            if(!shouldDispense) {
                shouldDispense = false;
            }
        } else {
            shouldDispense = true;
            StartCoroutine(dispense());
        }
    }

    private void reset() {
        choiceMode.reset();
        frequencyMultiplier = 1f;
    }

    public void OnDestroy() {
        shouldDispense = false;
    }

    private IEnumerator dispense() {
        if (!dispensingAlready) {
            dispensingAlready = true;
            while (shouldDispense) {
                shootttt();
                yield return new WaitForSeconds(baseFrequency * frequencyMultiplier);
            }
            dispensingAlready = false;
        }
    }

	public void increaseFrequency(DuckHitInfo dhi) {
        if (!startedMaxFrequency) {
            frequencyMultiplier = Mathf.Max(maxFrequency, frequencyMultiplier * 0.94f);
        }
        if (frequencyMultiplier < .04f) {
            StartCoroutine(resetFrequencyAfterATime());
        }
        updateSpeed(true);
        udpateIntensity();
	}

    private IEnumerator resetFrequencyAfterATime() {
        if (startedMaxFrequency) {
            yield return null;
        } else {
            startedMaxFrequency = true;
            yield return new WaitForSeconds(duckBarageTimeSeconds);
            frequencyMultiplier = 1f;
            startedMaxFrequency = false;
        }
    }

    private void udpateIntensity() {
        int total = 0;
        for(int i = 0; i < intensityMilestoneIntervals.Length; ++i) {
            total += intensityMilestoneIntervals[i];
            if (ScoreKeeper.Instance.getScore() > total) {
                continue;
            }
            choiceMode.setIntensity(i);
            break;
        }
    }

    private void decreaseFrequency(Duck duck) {
        frequencyMultiplier = Mathf.Min(1f, frequencyMultiplier * 1.05f);
        updateSpeed(false);
    }

    private void updateSpeed(bool positive) {
        if (positive) {
            speedMultiplier = Mathf.Min(1.9f, speedMultiplier + .1f);
        } else {
            speedMultiplier = Mathf.Max(1f, speedMultiplier - .05f);
        }
    }

	private void shootttt() {
        Duck d = Instantiate<Duck>(originals[choiceMode.getPick()]); 
        d.transform.parent = null;
		d.transform.position = transform.position;
        d.isClone = true;
        d.speed *= speedMultiplier;
		d.gameObject.SetActive (true);
	}


}

//TODO: cooldown the duck hordes even more.
// make ducks harder to hit even more

//[System.Serializable]
//public struct Ratio
//{
//    public int numerator;
//    public int denominator;

//    private float numer { get { return (float)numerator; } }
//    private float denom { get { return (float)denominator; } }

//    public float ratio { get { return numer / denom; } }
//}

[System.Serializable]
public struct DuckOriginals
{
    public Duck testDuck;
    public Duck duck;
    public BobbingDuck bobbingDuck;
    public RocketDuck rocketDuck;
    public EvilDuck evilDuck;
    public BossDuck bossDuck;
    public MongWeasel ratOnBike;

    public Duck this[int i] {
        get {
            if (testDuck) return testDuck;
            switch (i) {
                case 0:
                    return duck;
                case 1:
                    return bobbingDuck;
                case 2:
                    return rocketDuck;
                case 3:
                    return evilDuck;
                case 4:
                    return dispenseBoss ? bossDuck : duck;
                case 5:
                    return dispenseRatOnBike ? ratOnBike : duck;
                default:
                    break;
            }
            return null;
        }
    }

    public bool dispenseBoss {
        get {
            int r = Mathf.FloorToInt(Time.deltaTime * 10) * 100;
            int t = Mathf.RoundToInt(Time.deltaTime * 1000) - r; 
            if (t < (int) (15f * (Mathf.Clamp01(ScoreKeeper.Instance.getScore()/10000f) + 1f))) {
                return true;
            }
            return false;
        }
    }

    public bool dispenseRatOnBike {
        get {
            
            int r = Mathf.FloorToInt(Time.deltaTime * 10) * 100;
            int t = Mathf.RoundToInt(Time.deltaTime * 1000) - r; 
            if (t < (int) (7f * (Mathf.Clamp01(ScoreKeeper.Instance.getScore()/50000f) + .5f))) {
                return true;
            }
            return false;
        }
    }
}
