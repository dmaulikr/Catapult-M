using UnityEngine;
using System.Collections;
using System;

public class BossDuck : Duck {

    protected static int maxHitPoints = 20;
    protected int hitPoints = maxHitPoints;
    [SerializeField , Range(0, 1)]
    protected float slow = .3f;
    protected ColorLerp colorLerp;
    SpriteRenderer sr;

    public override void Awake() {
        base.Awake();
        colorLerp = GetComponent<ColorLerp>();
        colorLerp.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override int evilness {
        get {
            return 30;
        }
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
        colorLerp.set(1f - percentage);
        //colorLerp.strobeStartEnd();
    }
}
