using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : Singleton<AudioManager> {
    protected AudioManager() { }

    Dictionary<string, AudioSource> audios = new Dictionary<string, AudioSource>();

    public static string Dink = "Dink";
    public static string Zap = "Zap";
    public void Awake() {
        foreach(AudioSource au in GetComponentsInChildren<AudioSource>()) {
            audios.Add(au.gameObject.name, au);
        }
    }

    public void playDink() { play(Dink); }

    public void play(string name) {
        if (audios[name]) {
            audios[name].Play();
        }
    }

    internal void playZap() {
        play(Zap);
    }
}
