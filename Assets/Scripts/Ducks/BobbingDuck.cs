using UnityEngine;
using System.Collections;

public class BobbingDuck : Duck {

	public override void Awake() {
        bobMovement = true;
        base.Awake();
    }

    public override int evilness {
        get {
            return 0;
        }
    }
}
