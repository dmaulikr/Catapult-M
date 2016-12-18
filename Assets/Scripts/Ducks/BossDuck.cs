using UnityEngine;
using System.Collections;
using System;

public class BossDuck : Duck {
    [SerializeField]
    protected int maxHitPoints = 3;
    protected int hitPoints;
    protected HitPointBar hitPointBar;

    [SerializeField , Range(0, 1)]
    protected float slow = .3f;
    protected ColorLerp colorLerp;
    SpriteRenderer sr;

    protected Animator animator;

    public override void Awake() {

        base.Awake();
        hitPoints = maxHitPoints;
        hitPointBar = GetComponentInChildren<HitPointBar>();
        colorLerp = GetComponent<ColorLerp>();
        colorLerp.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    public override int evilness {
        get {
            return 30;
        }
    }

    public override bool getHit(Boulder boulder) {
        hitPoints -= boulder.preciousness;
        hitPointBar.setPercentage(percentage);
        if (hitPoints > 0) {
            status(hitPoints);
            print(hitPoints);
            return false;
        }
        if (isDead) {
            print("dead");
            animator.SetBool("isDead", true);
        }
        bool result = base.getHit(boulder);
        return isDead;
    }

    protected bool isDead { get { return percentage < .0001f; } }

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
