using UnityEngine;
using System.Collections;

public class EnlargeForMobile : MonoBehaviour {

    public float scaleBy = 1.5f;
	// Use this for initialization
	void Awake () {
        print("hi");
#if UNITY_IOS || UNITY_ANDROID
        print("scale");
        RectTransform rt = GetComponent<RectTransform>();
        if(rt) {
            print("found rt");
            //rt.rect.Set(rt.rect.x, rt.rect.y, rt.rect.x * scaleBy, rt.rect.y * scaleBy);
            rt.sizeDelta = rt.rect.size * scaleBy;
        }
#endif
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
