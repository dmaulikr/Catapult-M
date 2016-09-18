using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {

	protected Animator animator;
	protected AudioSource audioSource;
	[SerializeField]
	protected SpriteStrobe bgStrobe;
    protected AudioSource neverHitAudio;

	void OnEnable() {
		Duck.OnGotAHit += celebrate;
        Duck.OnNeverGotHit += acknowledgeNeverHit;
	}

	void OnDisable() {
		Duck.OnGotAHit -= celebrate;
        Duck.OnNeverGotHit -= acknowledgeNeverHit;
	}

	protected void celebrate(Boulder boulder) {
		animator.SetBool ("Excited", true);
		audioSource.Play ();
		//bgStrobe.strobe ();
	}

	public void beDisappointed() {
		StartCoroutine (disappointed ());
	}

	private IEnumerator disappointed() {
		animator.SetBool ("Ashamed", true);
		yield return new WaitForSeconds (1f);
		animator.SetBool ("Ashamed", false);
	}

	public void ashamedEnded() {
	}

    private void acknowledgeNeverHit(Duck duck) {
        //neverHitAudio.Play();
    }

	public void Awake () {
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
        foreach(AudioSource au in GetComponentsInChildren<AudioSource>()) {
            if (au == audioSource) { continue; }
            neverHitAudio = au;
            break;
        }
	}
	
	void Update () {
		
	}

	public void animationEnded() {
		animator.SetBool ("Excited", false);
	}
}
