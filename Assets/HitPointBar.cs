using UnityEngine;
using System.Collections;

public class HitPointBar : MonoBehaviour {
    

    public void setPercentage(float p) {
        print(p);
        Vector3 scale = transform.localScale;
        scale.Scale(new Vector3(Mathf.Clamp01(p), 1f, 1f));
        transform.localScale = scale;
    }
}
