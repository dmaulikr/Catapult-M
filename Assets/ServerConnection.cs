using UnityEngine;
using System.Collections;
using System;

public class ServerConnection : Singleton<ServerConnection> {
    protected ServerConnection() { }

    private string highscore_url = "http://duks.io/lb/req.py";

    public IEnumerator SubmitScore() {
        yield return SubmitScore(null);
    }             
       
	public IEnumerator SubmitScore (Action<String> receive) {
		// Create a form object for sending high score data to the server
		WWWForm form = new WWWForm();
		// Assuming the script manages high scores for different games
		form.AddField( "submitScore", "duksio" );
        // The name of the player submitting the scores
        string name = PlayerBehaviourData.Instance.playerName;
        if(string.IsNullOrEmpty(name)) {
            name = PlayerBehaviourData.Instance.randomName();
        }
		form.AddField( "name", name );
		// The score
		form.AddField( "score", ScoreKeeper.Instance.getScore() );

		// Create a download object
		WWW download = new WWW( highscore_url, form );
		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		} else {
            if(receive != null) {
                receive.Invoke(download.text);
            }
			
		}
	}

    public IEnumerator Available(Action<String> receive) {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();
		// Assuming the script manages high scores for different games
		form.AddField( "submitScore", "duksio" );
        // The name of the player submitting the scores
        string name = PlayerBehaviourData.Instance.playerName;
        if(string.IsNullOrEmpty(name)) {
            name = PlayerBehaviourData.Instance.randomName();
        }
		form.AddField( "name", name );
		// The score
		form.AddField( "score", ScoreKeeper.Instance.getScore() );

		// Create a download object
		WWW download = new WWW( highscore_url, form );
		// Wait until the download is done
		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		}
        string result = "";
        if(download != null && !string.IsNullOrEmpty(download.text)) {
            result = download.text;
        }
        if(receive != null) {
            receive.Invoke(download.text);
        }
	}

}
