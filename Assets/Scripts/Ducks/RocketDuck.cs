using UnityEngine;
using System.Collections;

public class RocketDuck : Duck {

    public AnimationCurve curve;

    protected override void move() {
        rb.AddForce(new Vector2(speed * curve.Evaluate(Mathf.Max(0f, normalizedPositionX)), 0f));
    }
}
