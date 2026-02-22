using System.Collections;
using UnityEngine;

public class OneShotSFX : MonoBehaviour
{
private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        PlayAndDestroy();
    }
    public void PlayAndDestroy() {
         audioSource.Play();
         Destroy(gameObject, audioSource.clip.length);
    }

}
