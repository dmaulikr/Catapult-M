using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class HitsInARow : MonoBehaviour
{
    private int _hits;
    [SerializeField]
    private int goal = 3;
    [SerializeField]
    private Stars stars;

    public void resetHits() {
        _hits = 0;
    }

    public void addHit(DuckHitInfo dhi, System.Action metGoalCallback) {
        _hits++;
        StartCoroutine(starDuck(dhi.duck, _hits));
        if (_hits >= goal) {
            metGoalCallback.Invoke();
            resetHits();
        }
    }

    private IEnumerator starDuck(Duck duck, int _hits) {
        Stars s = Instantiate<Stars>(stars);
        s.transform.position = duck.transform.position + Vector3.forward * -1f;
        s.makeStars(_hits);
        yield return new WaitForSeconds(.4f);
        Destroy(s.gameObject);
    }
}
