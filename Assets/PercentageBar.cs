using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PercentageBar : MonoBehaviour {

    public Color fullColor = Color.green;
    public Color emptyColor = Color.red;
    public Color warningPulseColor = Color.yellow;
    public float warningThreshhold = .5f;

    protected RectTransform bar;
    protected Image image;

    [SerializeField]
    private Text displayText;
    [SerializeField]
    private string label;

    [SerializeField]
    private Animator glowBar;
    private AudioSource aud;

    public void Awake() {
        aud = GetComponent<AudioSource>();
        bar = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        set(bar.localScale.x);
    }

    public void set(float f) {
        if (bar)
            bar.localScale = new Vector3(Mathf.Clamp01(f), 1f, 1f);
        if (image)
            image.color = Color.Lerp(emptyColor, fullColor, bar.localScale.x);
    }

    public void setNumbers(int current, int max) {
        displayText.text = string.Format("{0}: {1}/{2}", label, current, max);
    }

    public void glow() {
        glowBar.SetBool("isGlowTime", true);
        if(aud) {
            aud.Play();
        }
    }

    public void doneGlowing() {
        glowBar.SetBool("isGlowTime", false);
    }

    public void Update() {
        if (bar.localScale.x < warningThreshhold) {
            image.color = Color.Lerp(image.color, warningPulseColor, Mathf.PingPong(Time.time, 1));
        }
    }
    
    
}
