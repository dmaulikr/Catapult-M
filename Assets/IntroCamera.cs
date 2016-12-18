using UnityEngine;
using System.Collections;
using System;

public class IntroCamera : MonoBehaviour {

    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    [SerializeField]
    private float zoomSpeed = 40;
    [SerializeField]
    private Camera gameCam;

    public void Awake() {
        StartCoroutine(zoomAndHandoff());
    }

    private IEnumerator zoomAndHandoff() {
        transform.position = start.position;
        Vector3 dif = end.position - start.position;
        //float distTwo = dif.sqrMagnitude;
        Vector3 norm = dif.normalized;
        do {
            transform.position += norm * zoomSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        } while (Vector3.Dot(dif, end.position - transform.position) > 0f);
        GetComponent<Camera>().enabled = false;
        gameCam.gameObject.SetActive(true);
        gameCam.enabled = true;
        gameObject.SetActive(false);
    }
}
