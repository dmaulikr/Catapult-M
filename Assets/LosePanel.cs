using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LosePanel : MonoBehaviour {

	public Text lossReason;
	protected AudioSource audioSource;

	public void show(string reason) {
		gameObject.SetActive (true);
		lossReason.text = reason;
	}

	public void hide() {
		gameObject.SetActive (false);
	}
}
