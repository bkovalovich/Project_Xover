using UnityEngine;

public class ParticleOneshot : MonoBehaviour
{
    private ParticleSystem ps;

    public void Awake() {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnEnable() {
        ps.Play();
    }
    private void Update() {
        if (!ps.isPlaying) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
