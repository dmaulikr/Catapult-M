using UnityEngine;
using System.Collections;

public class Trophy : MonoBehaviour {

	public string title;
	public int requiredDucks;
	public bool achieved;

	public Sprite sprite;

    

	public void Awake() {
		sprite = GetComponentInChildren<SpriteRenderer> ().sprite;
	}

	public void appear() {
		GetComponentInChildren<SpriteRenderer> ().enabled = true;
	}

    public void hide() {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

}
