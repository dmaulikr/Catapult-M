using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {

	protected Animator animator;
	protected AudioSource audioSource;
	[SerializeField]
	protected SpriteStrobe bgStrobe;
    protected AudioSource neverHitAudio;
    [SerializeField]
    private Transform sneaks;
    internal bool hasSneaks {
        get {
            return sneaks.gameObject.activeSelf;
        }
    }

    public Transform giveSneaks() {
        if(!hasSneaks) { return null; }
        Transform sneaksCopy = Instantiate<Transform>(sneaks);
        sneaksCopy.position = sneaks.position;
        //sneaksCopy.localScale = transform.localScale;
        sneaks.gameObject.SetActive(false);
        return sneaksCopy;
    }

    public void getBackSneaks(Transform sneakCopy) {
        Destroy(sneakCopy.gameObject);
        sneaks.gameObject.SetActive(true);
    }

    void OnEnable() {
		Duck.OnDuckGotHit += celebrate;
        Duck.OnNeverGotHit += acknowledgeNeverHit;
        ScoreKeeper.Instance.OnReset += reset;
	}

	void OnDisable() {
        Duck.OnDuckGotHit -= celebrate;
        Duck.OnNeverGotHit -= acknowledgeNeverHit;
        if (ScoreKeeper.Instance != null) {
            ScoreKeeper.Instance.OnReset -= reset;
        }
	}

	protected void celebrate(DuckHitInfo boulder) {
		animator.SetBool ("Excited", true);
		audioSource.Play ();
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

    private void reset() {
        audioSource.Play();
    }


	public void animationEnded() {
		animator.SetBool ("Excited", false);
	}
}
