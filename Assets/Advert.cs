using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Advert : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDRIOD

    public void Start() {
        if (timeToShowAdOnStart()) {
            StartCoroutine(waitThenShow());
        }
    }

    public IEnumerator waitThenShow() {
        while (!Advertisement.IsReady()) {
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(ShowAd());
    }
    
    internal bool timeToShowAdOnStart() {
        return PlayerBehaviourData.Instance.openedAppCount > 5 && PlayerBehaviourData.Instance.openedAppCount % 3 == 0;
    }

    private bool timeToShowAdOnUnpause() {
        return !timeToShowAdOnStart() && PlayerBehaviourData.Instance.unpauses > 0 && PlayerBehaviourData.Instance.unpauses % 16 == 0;
    }

    public IEnumerator ShowAd() {
        if (Advertisement.IsReady()) {
            var options = new ShowOptions { resultCallback = addDone };
            Advertisement.Show(null, options);
            while(Advertisement.isShowing) {
                Time.timeScale = .0f;
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    public void OnApplicationPause(bool pause) {
        if(!pause && timeToShowAdOnUnpause()) {
            StartCoroutine(waitThenShow());
        } 
    }

    private void addDone(ShowResult result) {
        Time.timeScale = 1f;
    }
#endif
}


