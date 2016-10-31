using UnityEngine;
using System.Collections;

public class EmpoweredBoulder : Boulder {

    [SerializeField]
    protected Vector2 bounceBackScale = new Vector2(1.3f, 1.5f);
    protected override void awake() {
        base.awake();
    }

    protected override bool canHandleEvilness(Duck duck) {
        return true; // duck.evilness < 100;
    }

}
