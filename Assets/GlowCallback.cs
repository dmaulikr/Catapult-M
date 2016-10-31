using UnityEngine;
using System.Collections;

public class GlowCallback : MonoBehaviour {

    private PercentageBar percentageBar;

    public void Awake() {
        percentageBar = GetComponentInParent<PercentageBar>();
    }

    public void glowCallBack() {
        percentageBar.doneGlowing();
    }
}
