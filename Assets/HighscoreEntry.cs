using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreEntry : MonoBehaviour {

    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text rank;

    public void setNameScore(string n, string sco, string _rank) {
        _name.text = n;
        score.text = sco;
        rank.text = _rank;
    }
}
