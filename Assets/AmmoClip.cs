using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AmmoClip : Singleton<AmmoClip> {

    protected AmmoClip() { }

    public float refillInterval = 5f;
    public PercentageBar percentageBar;

    public int maxAmmo = 50;
    private int _ammo;
    private int ammo {
        get { return _ammo; }
        set {
            _ammo = value;
            percentageBar.set(percentageAmmo);
        }
    }

    protected float percentageAmmo {
        get {
            return (float)_ammo / (float)maxAmmo;
        }
    }

	public void Awake () {
        setup();
        StartCoroutine(refill());
	}

    private IEnumerator refill() {
        while(true) {
            yield return new WaitForSeconds(refillInterval);
            ammo++;
        }
    }

    public void OnEnable() {
        ScoreKeeper.Instance.OnReset += setup;
    }
    public void OnDisable() {
        if (ScoreKeeper.Instance && ScoreKeeper.Instance.OnReset != null) {
            ScoreKeeper.Instance.OnReset -= setup;
        }
    }

    private void setup() {
        ammo = maxAmmo;
    }

    public bool available {
        get { return ammo > 0; }
    }

    public bool getAmmo() {
        if (available) {
            ammo--;
            return true;
        }
        return false;
    }

    public void addAmmo(int amt) {
        int t = ammo + amt;
        if (t > maxAmmo) t = maxAmmo;
        else if (t < 0) t = 0;
        ammo = t;
    }
	
	void Update () {
	
	}
}
