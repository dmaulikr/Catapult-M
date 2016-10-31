using UnityEngine;
using System.Collections;
using System;

public class BoulderGunBoulder : BounceBoulder {

    public Boulder prefabSecondary;

    protected override void reactToHit(Duck duck) {
        base.reactToHit(duck);
    }

    public override void doLaunchRoutine() {
        StartCoroutine(boulderUpdate());
    }

    private IEnumerator boulderUpdate() {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(.5f);
        secondaryShot();
    }

    private void secondaryShot() {
        Boulder secondary = Instantiate<Boulder>(prefabSecondary);
        secondary.gameObject.SetActive(true);
        Vector3 startPos = transform.position - Vector3.up * 2;
        Vector3 offset = Vector3.up * 4f + Vector3.right * -3f;
        Vector2 dir = CatapultClicky.forceToReach(secondary.GetComponent<Rigidbody2D>(), startPos, startPos + offset);

        CatapultClicky.throwBoulder(secondary, transform.position + Vector3.up * 3, dir);
        callBack(new BoulderCallbackInfo());
    }

    public void OnDestroy() {
        callBack(new BoulderCallbackInfo());
    }
}
