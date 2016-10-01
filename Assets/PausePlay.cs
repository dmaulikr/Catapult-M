using UnityEngine;
using System.Collections;
using System;

public class PausePlay : MonoBehaviour {
    /*
     * NOT IN USE (or working: timeScale stops input also)
     * */
    [SerializeField]
    private RectTransform pausePanel;

    public void Awake() {
        pausePanel.gameObject.SetActive(false);
    }

    public void handlePause(bool pause) { 
        if(!pause) {
            StartCoroutine(waitForTapOrClick());
        }
    }

    private IEnumerator waitForTapOrClick() {
        pausePanel.gameObject.SetActive(true);
        print("wait for tap click");
        //Time.timeScale = 0f; //TODO: pause game without freezing input, etc.
        while (true) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) {
                break;
            }
            yield return new WaitForSecondsRealtime(.1f);
        }
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }
}
