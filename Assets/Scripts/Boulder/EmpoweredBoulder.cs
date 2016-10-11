using UnityEngine;
using System.Collections;

public class EmpoweredBoulder : Boulder {

    protected override void awake() {
        base.awake();
    }

    protected override bool canHandleEvilness(Duck duck) {
        return duck.evilness < 100;
    }
}
