using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetFonts : MonoBehaviour {

    [SerializeField]
    private Font templateFont;

    public void Awake() {
        if (templateFont) {
            foreach (Text t in GetComponentsInChildren<Text>()) {
                t.font = templateFont;
            }
        }
    }
}
