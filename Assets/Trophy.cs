using UnityEngine;
using System.Collections;
using System;



public class Trophy : MonoBehaviour {

	public string title;
	public int requiredDucks;
	public bool achieved;
	public Sprite sprite;

    public Upgrade upgrade;

	public void Awake() {
		sprite = GetComponentInChildren<SpriteRenderer> ().sprite;
        upgrade = GetComponent<Upgrade>();
	}

    public void award() {
        upgrade.giveUpgrade();
    }

	public void appear() {
		GetComponentInChildren<SpriteRenderer> ().enabled = true;
	}

    public void hide() {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

}
