using UnityEngine;
using System.Collections;
using System;

public class ColorLerp : MonoBehaviour {

    public Color start = Color.white;
    public Color end = Color.red;
    public SpriteRenderer spriteRenderer;
    private bool shouldStrobe;

    public void Awake() {
        if (spriteRenderer == null) {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void set(float f) {
        shouldStrobe = false;
        spriteRenderer.color = Color.Lerp(start, end, Mathf.Clamp01(f));
        StartCoroutine(strobe(spriteRenderer.color, Color.Lerp(spriteRenderer.color, Color.Lerp(start, end, Mathf.Clamp01(1f - f)), Mathf.Clamp01(f - 0.1f))));
    }

    private IEnumerator strobe(Color color1, Color color2) {
        shouldStrobe = true;
        for (int i = 0; i < 6; ++i) {
            spriteRenderer.color = i % 2 == 1 ? color1 : color2;
            yield return new WaitForFixedUpdate();
        }
    }
}
