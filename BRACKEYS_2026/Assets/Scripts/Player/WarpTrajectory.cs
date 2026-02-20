using UnityEngine;

public class WarpTrajectory : MonoBehaviour
{
    private SpriteRenderer sr; 
    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false; 
    }

    public void ToggleWarp(bool val) {
        if(val == false) {
            transform.position = Vector3.zero; 
        }
        sr.enabled = val; 

    }
}
