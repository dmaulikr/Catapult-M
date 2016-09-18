using UnityEngine;
using System.Collections;
using System;

public class Zapper : MonoBehaviour {

    public bool isClone = true;
    
    public void Awake() {
        if (isClone) {
            //StartCoroutine(waitThenDisappear());
        }
    }

    private IEnumerator waitThenDisappear() {
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }
}
