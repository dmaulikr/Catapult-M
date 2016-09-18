using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class SpriteStrobe : MonoBehaviour {

	SpriteRenderer sr;
    Image image;
	public Color strobeColor = Color.yellow;
	private Color defaultColor;
    // Use this for initialization

    private float lastStrobeTime;
    [SerializeField]
    private int flashCount = 3;
    [SerializeField]
    protected float strobeInterval = .02f;
	private bool shouldStrobe;
    private int strobeOn;

    void Start () {
        sr = GetComponent<SpriteRenderer>();
        if (sr) {
            defaultColor = sr.color;
        } else {
            image = GetComponent<Image>();
            defaultColor = image.color;
        }
	}

	public void strobe() {
		lastStrobeTime = Time.fixedTime;
		shouldStrobe = true;
        StartCoroutine(strobeTime());
	}

    private IEnumerator strobeTime() {
        for(int i = 0; i < flashCount; ++i) {
            if (sr) {
                sr.color = i % 2 == 0 ? strobeColor : defaultColor;
            } else {
                image.color = i % 2 == 0 ? strobeColor : defaultColor;
            }
            yield return new WaitForSeconds(Mathf.Max(Time.fixedDeltaTime, strobeInterval));
        }
        if (sr)
            sr.color = defaultColor;
        else
            image.color = defaultColor;
    }

}
