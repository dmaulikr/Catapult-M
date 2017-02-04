using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public enum AdCompletionStatus
{
    COMPLETED, NOT_COMPLETED, AD_NOT_ACTUALLY_SHOWN
};


public class Advert : MonoBehaviour 
{
    private Action<AdCompletionStatus> _onAdDone;

    public void showAd(UnpauseEvent unpauseEvent, Action<AdCompletionStatus> onAdDone) {
        _onAdDone = onAdDone;
#if UNITY_IOS || UNITY_ANDRIOD
        if (timeToShowAd(unpauseEvent)) {
            StartCoroutine(waitThenShow());
        } else {
            if(_onAdDone != null) { _onAdDone.Invoke(AdCompletionStatus.AD_NOT_ACTUALLY_SHOWN);  }
        }
#else
        advertisementDone(AdCompletionStatus.AD_NOT_ACTUALLY_SHOWN); 
#endif
    }

    private bool timeToShowAd(UnpauseEvent unpauseEvent) {
        switch (unpauseEvent) {
            case UnpauseEvent.APP_AWOKE:
                return timeToShowAdOnStart();
            case UnpauseEvent.ON_APP_UNPAUSE:
            default:
                return timeToShowAdOnUnpause();
        }
    }

#if UNITY_IOS || UNITY_ANDRIOD


    private IEnumerator waitThenShow() {
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

    private IEnumerator ShowAd() {
        if (Advertisement.IsReady()) {
            var options = new ShowOptions { resultCallback = addDone };
            Advertisement.Show(null, options);
            while(Advertisement.isShowing) {
                Time.timeScale = 0f;
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    private void addDone(ShowResult result) {
        Time.timeScale = 1f;
        AdCompletionStatus completionStatus = result == ShowResult.Finished ? AdCompletionStatus.COMPLETED : AdCompletionStatus.NOT_COMPLETED;
        if(_onAdDone != null) { _onAdDone.Invoke(completionStatus);  }
    }
#endif
}


