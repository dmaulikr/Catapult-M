using UnityEngine;

public class Stars : MonoBehaviour {
    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    public Color[] colors;

    public void Awake() {
        particles.gameObject.SetActive(false);
    }

    public void makeStars(int score) {
        particles.gameObject.SetActive(true);
    }
}
