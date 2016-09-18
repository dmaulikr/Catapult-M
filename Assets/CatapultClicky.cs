using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatapultClicky : MonoBehaviour 
{
	//public Boulder boulderOriginal;
    public Boulder[] boulderPrefabs;
    public TrophyShelf trophyShelf;

	public float strength = 5000f;
    protected Boulder boulderInWaiting;
    private List<float> requests = new List<float>();
    private BoulderChoiceMode choiceMode = new BoulderChoiceMode();
    private bool awaitingCallback;
    private float awaitCallbackTimeSeconds;

    public void Awake() {
        foreach(Boulder b in boulderPrefabs) {
            b.gameObject.SetActive(false);
        }
    }

    public void Update() {
        if (!awaitingCallback && Input.GetKeyDown(KeyCode.Space)) {
            requests.Add(Time.realtimeSinceStartup);
        }
        else if (awaitingCallback) {
            if (Time.realtimeSinceStartup - awaitCallbackTimeSeconds > 1f) { //fail-safe
                awaitingCallback = false;
            }
        }
    }

    private Boulder getNextBoulder() {
        choiceMode.setIntensity(trophyShelf.achievementLevel);
        return boulderPrefabs[choiceMode.getPick()];
    }

	public void FixedUpdate() {
		if (boulderInWaiting == null && AmmoClip.Instance.getAmmo()) {
            boulderInWaiting = Instantiate<Boulder>(getNextBoulder());
            boulderInWaiting.postLaunchCallback = postLaunchCallback;
            boulderInWaiting.GetComponent<Rigidbody2D>().gravityScale = 0;
            boulderInWaiting.transform.position = transform.position;
            boulderInWaiting.gameObject.SetActive(true);
        }
		if (shouldShoot() && boulderInWaiting != null) {
            throwBoulder(boulderInWaiting, transform.position, new Vector2(strength, strength));
            awaitingCallback = true;
            awaitCallbackTimeSeconds = Time.realtimeSinceStartup;
            boulderInWaiting = null;
		}
	}

    public static void throwBoulder(Boulder theNewBoulder, Vector2 startPos, Vector2 direction) {
        Rigidbody2D theNewBouldersRB = theNewBoulder.GetComponent<Rigidbody2D> ();
        theNewBouldersRB.velocity = Vector2.zero;
        theNewBouldersRB.gravityScale = 5;
        theNewBoulder.transform.position = startPos;
        theNewBouldersRB.AddForce (direction);
        theNewBoulder.doLaunchRoutine();
    }

    //delegate
    private void postLaunchCallback(Boulder.BoulderCallbackInfo bci) {
        StartCoroutine(waitForSpaceKeyUp());
    }

    private IEnumerator waitForSpaceKeyUp() {
        while(Input.GetKey(KeyCode.Space)) {
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        awaitingCallback = false;
    }

    private bool shouldShoot() {
        if (requests.Count > 0) {
            float requestTime = requests[requests.Count - 1];
            if (Time.realtimeSinceStartup - requestTime > Time.fixedDeltaTime) {
                requests.RemoveAt(requests.Count - 1);
                return true;
            }
        }
        return false;
    }

    public void OnDestroy() {
        print("catapult clicky destroy");
    }

}
