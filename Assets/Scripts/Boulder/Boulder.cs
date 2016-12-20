using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour , IDestructable {

	private bool gotOne; 
	[SerializeField]
	protected Shark shark;
    public int preciousness = 1;
    protected ParticleSystem _particleSystem;
    public delegate void PostLaunchCallback(BoulderCallbackInfo bci);
    public PostLaunchCallback postLaunchCallback;
    public Zapper zap;
    public BoulderType type;

    public void Awake() {
        awake();
    }
    protected virtual void awake() {
        gameObject.layer = LayerMask.NameToLayer("Boulder");
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        if (_particleSystem) {
            //_particleSystem.gameObject.SetActive(false);
        }
    }

	void OnCollisionEnter2D(Collision2D other) {
        handleCollisionEnter(other);
	}

    protected virtual void handleCollisionEnter(Collision2D other) {
		Duck ducky = other.transform.GetComponent<Duck> ();
		if (ducky != null && !ducky.didDie) {
            if (canHandleEvilness(ducky)) {
                AudioManager.Instance.playDink();
                if (ducky.getHit(this)) {
                    reactToHit(ducky);
                }
            } else {
                getStymiedByEvil(ducky);
            }
		}
    }

    protected virtual void getStymiedByEvil(Duck duck) {
        duck.rejectHit(this);
        StartCoroutine(getZapped(true));
    }

    protected IEnumerator getZapped(bool destroy) {
        Zapper zapper = Instantiate<Zapper>(zap);
        zapper.gameObject.SetActive(true);
        zapper.transform.position = transform.position;
        zapper.transform.parent = transform;
        AudioManager.Instance.playZap();
        yield return new WaitForSeconds(.4f);
        if (destroy) {
            Destroy(zapper.gameObject);
        }
        getDestroyed();
    }

    protected virtual bool canHandleEvilness(Duck duck) {
        return true; // duck.evilness < 1;
    }

    protected virtual void reactToHit(Duck duck) {
        gotOne = true;
        ScoreKeeper.Instance.decreaseAirballCount();
        showParticles();
    }


	public void getDestroyed() {
		if (!gotOne) {
			shark.beDisappointed ();
            ScoreKeeper.Instance.addAirball();
		}
		Destroy (this.gameObject);
	}

    protected void showParticles() {
        if(_particleSystem) {
            _particleSystem.gameObject.SetActive(true);
        }
    }

    public virtual void doLaunchRoutine() {
        callBack(new BoulderCallbackInfo());
    }

    protected void callBack(BoulderCallbackInfo bci) {
        if (postLaunchCallback != null) {
            postLaunchCallback(bci);
        }
    }

    public class BoulderCallbackInfo
    {
        
    }
}

[System.Serializable]
public enum BoulderType {
    BOULDER, EMPOWERED, BOULDER_GUN, BOOMERANG, BOMB
};