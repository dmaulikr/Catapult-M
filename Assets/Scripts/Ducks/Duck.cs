using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Duck : MonoBehaviour , IDestructable {
	
	public float speed = .1f;
	private bool gotHit = false;

	public delegate void HandleGotAHit (Boulder boulder);
	public static event HandleGotAHit OnGotAHit;

    public delegate void HandleNeverGotHit(Duck duck);
    public static event HandleNeverGotHit OnNeverGotHit;

    public bool isClone = false;

    protected bool bobMovement = false;

    [SerializeField]
    protected float period = 3f;

    [SerializeField]
    private float bobAmplitude = .1f;

    [SerializeField]
    protected int _evilness = 0;

    public int missPenalty = 1;

    public virtual int evilness {
        get { return _evilness; }
    }

    protected Rigidbody2D rb;

    public virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        doMove = move;
    }

    void OnEnable() {
        ScoreKeeper.Instance.OnReset += reset;
	}

    void OnDisable() {
        if (ScoreKeeper.Instance && ScoreKeeper.Instance.OnReset != null) {
            ScoreKeeper.Instance.OnReset -= reset;
        }
	}

    private void reset() {
        if (isClone) {
            Destroy(gameObject);
        }
    }

    protected float normalizedPositionX {
        get { return Mathf.Max(transform.position.x - WorldWidth.Instance.lineSegment.start.position.x, 0f) / WorldWidth.Instance.width; }
    }
    protected float bob {
        get { return bobMovement ? bobAmplitude * Mathf.Sin(Mathf.PI * 2f * normalizedPositionX * period) : 0f; }
    }
    protected Vector2 bobDelta {
        get { return new Vector2(0f, bob); }
    }

    protected virtual Vector2 force {
        get { return new Vector2(speed, 0f); }
    }

    protected delegate void DoMove();
    protected DoMove doMove;

    void FixedUpdate () {
        doMove();
		//if (!gotHit) {
  //          move();
		//}
	}

    protected virtual void move() {
        rb.MovePosition(getMove());
    }

    private void doNothing() { }

    protected virtual Vector2 getMove() {
        return rb.position + force + bobDelta;
    }

	public virtual bool getHit(Boulder boulder) {
		gotHit = true;
        doMove = doNothing;
        rb.constraints = RigidbodyConstraints2D.None;
		OnGotAHit (boulder);
		GetComponent<Rigidbody2D> ().gravityScale = 1f;
        return true;
	}

    public virtual void rejectHit(Boulder boulder) {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void getDestroyed() {
        if (!gotHit) {
            OnNeverGotHit(this);
        }
        if (isClone) {
            Destroy(gameObject);
        }
    }
}
