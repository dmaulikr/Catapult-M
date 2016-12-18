using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using System;


[Serializable]
public class ToggleEvent : UnityEvent<bool>
{

}

public class GameManager : Singleton<GameManager> {
    protected GameManager() { }

    public ToggleEvent gmPause;
    private bool isGMPaused;
    [SerializeField]
    private LeaderBoard lb;

    private bool inGamePaused;
    public RectTransform pauseScrim;

    public void Start() {
        for (int i = 0; i < gmPause.GetPersistentEventCount(); ++i) {
            gmPause.SetPersistentListenerState(i, UnityEventCallState.RuntimeOnly);
            ((MonoBehaviour)gmPause.GetPersistentTarget(i)).SendMessage(gmPause.GetPersistentMethodName(i), isGMPaused);
        }
    }

    public void pause() {
        isGMPaused = true;
        gmPause.Invoke(true);
    }

    public void unpause() {
        print("invoke unpause");
        isGMPaused = false;
        gmPause.Invoke(false);
    }
    
    public void inGamePause() {
        inGamePaused = !inGamePaused;
        Time.timeScale = inGamePaused ? 0f : 1f;
        pauseScrim.gameObject.SetActive(inGamePaused);
        showLeaderBoard(inGamePaused);
    }

    public void showLeaderBoard(bool show) {
        lb.gameObject.SetActive(show);
    }

}
