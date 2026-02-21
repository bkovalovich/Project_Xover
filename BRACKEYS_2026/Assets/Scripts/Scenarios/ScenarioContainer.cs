using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 
using System.Collections; 

public class ScenarioContainer : MonoBehaviour
{
    [SerializeField] public string ID; 
    [SerializeField] public Image overlay;
    [SerializeField] public ParticleSystem particles;
    private void Awake() {
        particles.gameObject.SetActive(false);
    }
}
