using UnityEngine;
using System.Collections;
using System;

public class BossDuck : Duck {

    protected const int maxHitPoints = 20;
    [SerializeField]
    protected int hitPoints = maxHitPoints;
    [SerializeField , Range(0, 1)]
    protected float slow = .3f;
    protected ColorLerp colorLerp;

    public override void Awake() {
        base.Awake();
        colorLerp = GetComponent<ColorLerp>();
    }

    public override bool getHit(Boulder boulder) {
        hitPoints -= boulder.preciousness;
        if (hitPoints > 0) {
            status(hitPoints);
            return false;
        }
        return base.getHit(boulder);
    }

    protected override Vector2 force {
        get {
            return base.force * slow;
        }
    }

    protected float percentage {
        get { return (float)hitPoints / (float)maxHitPoints; }
    }

    protected void status(int hitPoints) {
        colorLerp.set(percentage);
    }
}
