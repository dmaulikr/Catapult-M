using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using System;


public enum UnpauseEvent
{
    APP_AWOKE, ON_APP_UNPAUSE
};

//[Serializable]
//public class ToggleEvent : UnityEvent<bool>
//{

//}

public class GameManager : Singleton<GameManager> {
    protected GameManager() { }

    public delegate void GMPause(bool paused);
    public GMPause gmPause;
    //public ToggleEvent gmPause;
    private bool isGMPaused;
    [SerializeField]
    private LeaderBoard lb;

    private bool inGamePaused;
    public RectTransform pauseScrim;

    [SerializeField]
    private Advert advert;

//TODO: determine when to call restart (look at ScoreKeeper and possibly take over its job...)
//TODO: within restart process: when to pause() and unpause()
//TODO: make ducks helped count bigger and possibly animated to emphasize
//TODO: give evil stop-start ducks more space: they often make ducks in front of them unhittable 

    private void restart(UnpauseEvent unpauseEvent) {
        pause();
        advert.showAd(unpauseEvent, (AdCompletionStatus aCS) => {
            if (unpauseEvent == UnpauseEvent.APP_AWOKE) {
                PlayerIDManager.Instance.setupPlayerName(() => {
                    unpause();
                });
            } else {
                unpause();
            }
        });
    }

    public void Start() {
        //isGMPaused = true;
        ///* STICKY WICKET: This code does seem to help...but why do we need to this again? 
        //  (Without it, invoking gmPause(true) will passing false to the DuckDispenser method...) 
        //  TODO: go back to delegate instead           */
        //for (int i = 0; i < gmPause.GetPersistentEventCount(); ++i) {
        //    print("set callback state.");
        //    gmPause.SetPersistentListenerState(i, UnityEventCallState.RuntimeOnly);
        //    ((MonoBehaviour)gmPause.GetPersistentTarget(i)).SendMessage(gmPause.GetPersistentMethodName(i), isGMPaused);
        //}

        print("about to restart");
        restart(UnpauseEvent.APP_AWOKE);
    }

    public void OnApplicationPause(bool pause) {
        if (!pause) {
            advert.showAd(UnpauseEvent.ON_APP_UNPAUSE, (AdCompletionStatus aCS) => { });
        } 
    }

    #region inGamePause
    public void pause() {
        if(isGMPaused) { return; }
        print("invoke gmPause true");
        isGMPaused = true;
        if(gmPause != null) { gmPause(true); }
        //gmPause.Invoke(true);
    }

    public void unpause() {
        if(!isGMPaused) { return; }
        print("invoke gmPause false");
        isGMPaused = false;
        if(gmPause != null) { gmPause(false); }
        //gmPause.Invoke(false);
    }


    public void inGameUnPause() {
        doInGamePause(false);
    }
    
    public void inGamePause() {
        doInGamePause(true);
    }

    private void doInGamePause(bool pause) {
        inGamePaused = pause;
        Time.timeScale = inGamePaused ? 0f : 1f;
        pauseScrim.gameObject.SetActive(inGamePaused);
        showLeaderBoard(inGamePaused);
    }


    public void showLeaderBoard(bool show) {
        lb.gameObject.SetActive(show);
    }
    #endregion



}
