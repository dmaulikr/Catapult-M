using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementPanel : MonoBehaviour {

	public Text achievementName;
	public Image image;
	protected AudioSource audioSource;

	public void show(Trophy t) {
		gameObject.SetActive (true);
		achievementName.text = t.title;
		image.sprite = t.sprite;
		GetComponent<AudioSource> ().Play ();
	}

	public void hide() {
		gameObject.SetActive (false);
	}
}
