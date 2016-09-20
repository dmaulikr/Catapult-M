using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Advert : MonoBehaviour
{
     public void Start() {
        if (timeToShowAd()) {
            StartCoroutine(waitThenShow());
        }
    }

    public IEnumerator waitThenShow() {
        while (!Advertisement.IsReady()) {
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(ShowAd());
    }
    
    internal bool timeToShowAd() {
        return PlayerBehaviourData.Instance.openedAppCount > 0 && PlayerBehaviourData.Instance.openedAppCount % 3 == 0;
    }
    private bool timeToShowAdOnUnpause() {
        return PlayerBehaviourData.Instance.unpauses > 0 && PlayerBehaviourData.Instance.unpauses % 16 == 0;
    }

    public IEnumerator ShowAd() {
        if (Advertisement.IsReady()) {
            var options = new ShowOptions { resultCallback = addDone };
            Advertisement.Show(null, options);
            while(Advertisement.isShowing) {
                Time.timeScale = .001f;
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
        print("add done");
        Time.timeScale = 1f;
    }
}


