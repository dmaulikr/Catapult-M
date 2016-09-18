using UnityEngine;
using System.Collections;

public class Dove : Duck {

	public override bool getHit(Boulder boulder) {
		return base.getHit (boulder);
	}
}
