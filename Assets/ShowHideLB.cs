using UnityEngine;
using System.Collections;

public class ShowHideLB : MonoBehaviour {

    [SerializeField]
    private LeaderBoard lb;

	public void pressed() {
        GameManager.Instance.showLeaderBoard(!lb.gameObject.activeSelf);
    }
}
