using UnityEngine;
using System.Collections;

public class LeaderBoard : MonoBehaviour {
    [SerializeField]
    private HighscoreEntry rowPrefab;
    [SerializeField]
    private RectTransform container;

    public void show() {
        showBoard(false);
        StartCoroutine(ServerConnection.Instance.SubmitScore((string s) => {
            display(s);
        }));
    }

    public void hide() {
        showBoard(false);
    }

    public void OnEnable() {
        show();
    }

    private void display(string data) {
        string[] lines = data.Split('\n');
        if(string.IsNullOrEmpty(data) || lines.Length < 2) {
            showBoard(false);
            return;
        }
        showBoard(true);
        Vector2 pos = rowPrefab.GetComponent<RectTransform>().position;
        int rank = 1;

        foreach (string lin in lines) {
            string[] row = lin.Split(new string[] { ":^:^:" }, System.StringSplitOptions.None);
            if(row.Length != 2) { continue; }
            HighscoreEntry hen = Instantiate<HighscoreEntry>(rowPrefab);
            hen.gameObject.SetActive(true);
            hen.setNameScore(row[0], row[1], string.Format("{0}", rank++));
            RectTransform rt = hen.GetComponent<RectTransform>();
            rt.position = pos;
            pos.y -= rt.sizeDelta.y;
            rt.SetParent(container);
            if(rank >= 10) {
                break;
            }
        }
    }

    private void showBoard(bool show) {
        container.gameObject.SetActive(show);
    }


    public void OnDisable() {
        cleanUp();
    }

    private void cleanUp() {
        foreach(HighscoreEntry hen in GetComponentsInChildren<HighscoreEntry>()) {
            if (hen != rowPrefab) {
                Destroy(hen.gameObject);
            }
        }
    }
}
