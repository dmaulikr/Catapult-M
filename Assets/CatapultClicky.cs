using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatapultClicky : MonoBehaviour 
{
    public Boulder[] boulderPrefabs;
    public TrophyShelf trophyShelf;

	//public float strength = 5000f;
    protected Boulder boulderInWaiting;
    private List<float> requests = new List<float>();
    private BoulderChoiceMode choiceMode = new BoulderChoiceMode();
    [SerializeField]
    private Transform boulderTrajectoryTarget;

#if UNITY_IOS || UNITY_ANDRIOD
    private float lastTouchTime;
    [SerializeField]
    private float touchDownTimeSeconds = .2f;
#endif

    private Vector2 trajectory {
        get { return (boulderTrajectoryTarget.position - transform.position).normalized; }
    }

    public void Awake() {
        foreach(Boulder b in boulderPrefabs) {
            b.gameObject.SetActive(false);
        }
    }

    public void Update() {
#if UNITY_IOS || UNITY_ANDRIOD
        if (Mathf.Abs(Time.time - lastTouchTime) > touchDownTimeSeconds && Input.touchCount > 0) {
            lastTouchTime = Time.time;
            requests.Add(Time.realtimeSinceStartup);
        }
#else
        if (Input.GetKeyDown(KeyCode.Space)) {
            requests.Add(Time.realtimeSinceStartup);
        }
#endif
    }

    private Boulder getNextBoulder() {
        choiceMode.setIntensity(trophyShelf.achievementLevel);
        if(AmmoClip.Instance.deductAmmo()) {
            return Instantiate<Boulder>(boulderPrefabs[choiceMode.getPick()]);
        } else {
            requests.Clear(); 
        }
        return null;
    }

	public void FixedUpdate() {
		if (boulderInWaiting == null) {
            boulderInWaiting = getNextBoulder();
            if(boulderInWaiting) {
                boulderInWaiting.transform.position = transform.position;
                boulderInWaiting.gameObject.SetActive(true);
                boulderInWaiting.GetComponent<Rigidbody2D>().drag = 0f; //makes calc easier
                boulderInWaiting.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
		if (boulderInWaiting && shouldShoot()) {
            boulderInWaiting.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            throwBoulder(boulderInWaiting, transform.position, getForce(boulderInWaiting.GetComponent<Rigidbody2D>()));
            boulderInWaiting = null;
		}
	}

    private Vector2 getForce(Rigidbody2D boulderRB) {
        Vector2 dif = boulderTrajectoryTarget.position - transform.position;
        if (boulderRB.gravityScale == 0f) { boulderRB.gravityScale = 1f; }
        float y0 = Mathf.Sqrt(dif.y * -2 * Physics2D.gravity.y * boulderRB.gravityScale);
        float x0 = (-Physics2D.gravity.y * boulderRB.gravityScale / y0) * dif.x;
        return new Vector2(x0, y0);
    }

    public static void throwBoulder(Boulder theNewBoulder, Vector2 startPos, Vector2 direction) {
        Rigidbody2D theNewBouldersRB = theNewBoulder.GetComponent<Rigidbody2D> ();
        theNewBoulder.transform.position = startPos;
        theNewBouldersRB.AddForce(direction * theNewBouldersRB.mass, ForceMode2D.Impulse);
        theNewBoulder.doLaunchRoutine();
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

}
