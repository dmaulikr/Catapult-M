using UnityEngine;
using System.Collections;

public class EvilDuck : Duck
{
    public AnimationCurve curve;
    private float random01;
    [SerializeField]
    private float duckShimmyTimeScale = .02f;
    [SerializeField]
    private float xPosShimmyShift = .1f;

    public override void Awake() {
        base.Awake();
        random01 = Random.Range(0f, 1f);
    }

    public override int evilness {
        get {
            return 4;
        }
    }

    private float speedX {
        get {
            return speed * Mathf.Max(-1f, curve.Evaluate(Mathf.PingPong(
                normalizedPositionX * xPosShimmyShift +
                Time.deltaTime * duckShimmyTimeScale +
                random01, 1f)));
        }
    }

    protected override void move() {
        if (speedX < 0f && normalizedPositionX < .2f) {
            rb.AddForce(Vector2.right * .1f, ForceMode2D.Impulse);
        } else {
            rb.AddForce(new Vector2(speedX, 0f), ForceMode2D.Impulse);
        }
    }

    public override bool getHit(Boulder boulder) {
        bool result = base.getHit(boulder);
        rb.drag = .2f;
        rb.mass = 2f;
        return result;
    }

}
