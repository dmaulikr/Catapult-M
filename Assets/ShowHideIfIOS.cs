using UnityEngine;
using System.Collections;

public class ShowHideIfIOS : MonoBehaviour {

    public RectTransform showHide;
    public bool showIfIOS;

	// Use this for initialization
	void Start () {
        if(!showHide) {
            showHide = GetComponent<RectTransform>();
        }
        if(showHide) {
            bool enabled = false;
#if UNITY_IOS || UNITY_ANDROID
            enabled = showIfIOS;
#else
            enabled = !showIfIOS;
#endif

            showHide.gameObject.SetActive(enabled);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
