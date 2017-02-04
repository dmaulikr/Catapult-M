using UnityEngine;
using System.Collections;
using System;


public class PlayerBehaviourData : Singleton<PlayerBehaviourData> {

    protected PlayerBehaviourData() { }
    public int openedAppCount {
        get {
            if(PlayerPrefs.HasKey(PlayerKeys.LaunchedGameCount)) {
                return PlayerPrefs.GetInt(PlayerKeys.LaunchedGameCount);
            }
            return 0;
        }
    }

    public int unpauses {
        get {
            if(PlayerPrefs.HasKey(PlayerKeys.UnPauses)) {
                return PlayerPrefs.GetInt(PlayerKeys.UnPauses);
            }
            return 0;
        } 
        private set {
            PlayerPrefs.SetInt(PlayerKeys.UnPauses, value);
        }
    }

    public string randomName() {
        return string.Format("random-name{0:00000}", (int)UnityEngine.Random.value * 10000);
    }

    public string playerName { 
        get {
            if(PlayerPrefs.HasKey(PlayerKeys.PlayerName)) {
                return PlayerPrefs.GetString(PlayerKeys.PlayerName);
            }
            return null;
        }
        set {
            PlayerPrefs.SetString(PlayerKeys.PlayerName, value);
        }
    }

    void Start () {
        int times = 0;
        if(PlayerPrefs.HasKey(PlayerKeys.LaunchedGameCount)) {
            times = PlayerPrefs.GetInt(PlayerKeys.LaunchedGameCount);
        }
        PlayerPrefs.SetInt(PlayerKeys.LaunchedGameCount, ++times);
	}

    public void OnApplicationPause(bool pause) {
        if(!pause) { unpauses++; }
    }

}
