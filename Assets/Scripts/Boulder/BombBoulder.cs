using UnityEngine;
using System.Collections;
using System;

public class BombBoulder : EmpoweredBoulder {
    private Rigidbody2D rb;
    private Collider2D bombAreaCollider;
    [SerializeField]
    private ParticleSystem particles;
    private Animator anim;
    [SerializeField]
    private Transform bombRadius;

    private AudioSource aus;

    protected override void awake() {
        base.awake();
        aus = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //particles = GetComponentInChildren<ParticleSystem>();
        foreach (Transform t in transform) {
            if (t.CompareTag("Bomb")) {
                bombAreaCollider = t.GetComponent<Collider2D>();
                break;
            }
        }
        particles.gameObject.SetActive(false);
        activateBomb(false);
    }
    private void activateBomb(bool activate) {
        bombAreaCollider.gameObject.SetActive(activate);
    }

    public override void doLaunchRoutine() {
        StartCoroutine(flyAndExplode());
    }

    private IEnumerator flyAndExplode() {
        int safety = 0;
        do {
            yield return new WaitForFixedUpdate();
            if(safety++ > 20000) { break; }
        } while (rb.velocity.y <= 0f);

        do {
            yield return new WaitForFixedUpdate();
        } while (rb.velocity.y > 0f);

        StartCoroutine(explode());
    }

    private IEnumerator explode() {
        rb.gravityScale = .5f;
        rb.drag = 7f;
        yield return new WaitForSeconds(.3f);
        bombRadius.gameObject.SetActive(true);
        particles.gameObject.SetActive(true);
        anim.SetBool("Activated", true);
    }

    public void OnExplodeAnimationFinished() {
        activateBomb(true);
        StartCoroutine(waitAndDisapear());
    }

    private IEnumerator waitAndDisapear() {
        aus.Play();
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
