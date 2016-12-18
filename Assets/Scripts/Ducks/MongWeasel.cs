using UnityEngine;
using System.Collections;
using System;

public class MongWeasel : BossDuck
{

    [SerializeField]
    private Shark shark;
    [SerializeField]
    private Transform sneaksHolder;

    private Transform sneaks;

    public override void Awake() {
        base.Awake();
        if(shouldHaveSneaksAtStart()) {
            sneaks = shark.giveSneaks();
            clutchSneaks();
        }
        if(otherMongWeaselActive()) {
            Destroy(gameObject);
        }
    }

    public override bool getHit(Boulder boulder) {
        bool result = base.getHit(boulder);
        if(isDead) {
            giveBackSneaks();
        }
        return result;
    }

    private void clutchSneaks() {
        if(!sneaks) { return; }
        //sneaks.transform.position = sneaksHolder.position;
        sneaks.transform.parent = sneaksHolder;
        sneaks.transform.localPosition = Vector3.zero;
    }

    private bool shouldHaveSneaksAtStart() {
        if(shark.hasSneaks) { return false; }
        MongWeasel[] others = FindObjectsOfType<MongWeasel>();
        foreach(MongWeasel mw in others) {
            if(mw == this) {
                continue;
            }
            if(mw.hasSneaks) { return false; }
        }
        return true;
    }

    private bool otherMongWeaselActive() {
        foreach(MongWeasel mw in FindObjectsOfType<MongWeasel>()) {
            if (mw == this) { continue; }
            return true;
        }
        return false;
    }

    protected override void move() {
        base.move();
        acquireSneaks();
    }

    private float nearSharkWidth {
        get { return WorldWidth.Instance.width * .7f; }
    }

    private bool isNearShark {
        get { return Mathf.Abs(toShark.x) < nearSharkWidth; }
    }

    public bool hasSneaks {
        get {
            return sneaks != null;
        }
    }

    private void acquireSneaks() {
        if (!isNearShark) {
            return;
        }
        if(!shark.hasSneaks) { return; }
        StartCoroutine(grabSneaks());
    }

    private void giveBackSneaks() {
        shark.getBackSneaks(sneaks);
        sneaks = null;
    }

    private Vector3 toShark { get { return shark.transform.position - transform.position; } }
    private Quaternion sharkLook { get { return Quaternion.LookRotation(toShark); } }

    private IEnumerator grabSneaks() {
        
        sneaks = shark.giveSneaks();
        print("ac sneaks");
        if(sneaks) {
            Vector3 snpos = sneaks.position;
            sneaks.position = snpos;
            sneaks.parent = null;
            float increments = 100f;
            for (int i=0; i< (int)increments; ++i) {
                float grad = i / increments;
                sneaks.position = Vector3.Lerp(snpos, sneaksHolder.position, grad);
                yield return new WaitForFixedUpdate();
            }
            clutchSneaks();
        }
       
    }

   
}

