using UnityEngine;
using System.Collections; 

public class WarpTrajectory : MonoBehaviour {
    private SpriteRenderer sr;
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject burst; 

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        ToggleWarp(false);
    }

    public void ToggleWarp(bool val) {
        if (val == false) {
            transform.localPosition = Vector3.zero;
        }
        ps.gameObject.SetActive(val);
        sr.enabled = val;
    }
    public void Burst(Vector2 pos) {
        Instantiate(burst, pos, Quaternion.identity);
    }
}
