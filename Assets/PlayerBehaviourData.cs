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


    void Start () {
        int times = 0;
        if(PlayerPrefs.HasKey(PlayerKeys.LaunchedGameCount)) {
            times = PlayerPrefs.GetInt(PlayerKeys.LaunchedGameCount);
            print(times + "<-- returned to game times");
        }
        PlayerPrefs.SetInt(PlayerKeys.LaunchedGameCount, ++times);
	}

    public void OnApplicationPause(bool pause) {
        print("pause: " + pause);
        if(!pause) { unpauses++; }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
