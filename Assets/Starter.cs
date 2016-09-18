using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Starter : MonoBehaviour {

    public string levelName = "Untitled";
	public void onStartPressed() {
        SceneManager.LoadScene(levelName);
        //Application.LoadLevel(levelName);
    }
}
