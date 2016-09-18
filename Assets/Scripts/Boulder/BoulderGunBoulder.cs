using UnityEngine;
using System.Collections;
using System;

public class BoulderGunBoulder : EmpoweredBoulder {

    public Boulder prefabSecondary;
    private bool canShoot;

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
        canShoot = true;
    }

    private void secondaryShot() {
        if(prefabSecondary.gameObject.activeSelf) { return; }
        Boulder secondary = Instantiate<Boulder>(prefabSecondary);
        secondary.gameObject.SetActive(true);
        Vector2 normal = GetComponent<Rigidbody2D>().velocity;
        normal.x *= -50;
        normal.y =  Mathf.Abs(normal.x) * 5f;
        CatapultClicky.throwBoulder(secondary, transform.position, normal);
        callBack(new BoulderCallbackInfo());
    }

    public void Update() {
        if(canShoot && Input.GetKeyDown(KeyCode.Space)) {
            print("space");
            secondaryShot();
        }
    }

    public void OnDestroy() {
        callBack(new BoulderCallbackInfo());
    }
}
