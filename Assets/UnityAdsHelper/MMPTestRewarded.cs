using UnityEngine;
using System.Collections;

public class MMPTestRewarded : MonoBehaviour {

	public void handleSkipped() {
        print("they skipped");
    }

    public void handleFinished() {
        print("they watched it");
    }
}
