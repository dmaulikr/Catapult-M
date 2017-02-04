using UnityEngine;
using System.Collections;

public class EnlargeForMobile : MonoBehaviour {

    public float scaleBy = 1.5f;

	void Awake () {
#if UNITY_IOS || UNITY_ANDROID
        RectTransform rt = GetComponent<RectTransform>();
        if(rt) {
            rt.sizeDelta = rt.rect.size * scaleBy;
        }
#endif
    }

}
