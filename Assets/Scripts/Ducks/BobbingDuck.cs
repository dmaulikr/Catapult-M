using UnityEngine;
using System.Collections;

public class BobbingDuck : Duck {

	public override void Awake() {
        bobMovement = true;
        base.Awake();
    }
}
