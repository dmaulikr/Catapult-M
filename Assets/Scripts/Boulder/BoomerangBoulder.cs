using UnityEngine;
using System.Collections;
using System;

public class BoomerangBoulder : Boulder {

    public Vector2 forceDir = new Vector2(1f, 1f).normalized;
    public float turnPower = 10f;

    protected float lifeTime;

    protected Rigidbody2D rb;
    protected float coef = 13.5f;
    protected Transform sprite;
    private float spinSpeed = 12f;

    protected Vector2 direction {
        get {
            float normX = WorldWidth.Instance.normalizedXPosition01(transform.position.x);
            return new Vector2(-2f * normX * normX, 1f * (1.1f - normX)) * ((coef * lifeTime * lifeTime * lifeTime)/(1.1f - normX) - .5f); 
        }  
    }

    protected override void awake() {
        base.awake();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>().transform;
    }

    private void spinSprite() {
        sprite.transform.eulerAngles += new Vector3(0f, 0f, spinSpeed);
    }

    public override void doLaunchRoutine() {
        base.doLaunchRoutine();
        StartCoroutine(boomerang());
    }

    private IEnumerator boomerang() {
        while(true) {
            lifeTime += Time.fixedDeltaTime;
            rb.AddForce(direction * turnPower );
            spinSprite();
            yield return new WaitForFixedUpdate();
        }
    }

    protected override void getStymiedByEvil(Duck duck) {
        duck.rejectHit(this);
        StartCoroutine(getZapped(false));
    }

}
