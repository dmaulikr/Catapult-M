using UnityEngine;
using System.Collections;
using System;

public class FadeLogo : MonoBehaviour {

    private SpriteRenderer sr;
    [SerializeField]
    private float fadeSpeed = .05f;

    public void Awake() {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(fade());
    }

    private IEnumerator fade() {
        Color c = sr.color;
        do {
            c.a -= fadeSpeed;
            sr.color = c;
            yield return new WaitForEndOfFrame();
        } while (sr.color.a > .05f);
        Destroy(gameObject);
    }

}
