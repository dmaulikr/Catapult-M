using UnityEngine;
using System.Collections;

public class BombAnimCallback : MonoBehaviour {

    public void OnAnimationEnded() {
        GetComponentInParent<BombBoulder>().OnExplodeAnimationFinished();
    }
}
