using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image fill;
    ScenarioManager parent;
    private float maxTime;
    private float current;
    private Coroutine timerCoroutine;

    //private void Awake() {
    //    fill = transform.GetComponent<Image>(); 
    //}
    public void Toggle(bool val) {
        fill.enabled = val; 
    }
    public void StartTimer(float maxTime, ScenarioManager parent) {
        Toggle(true);
        this.maxTime = maxTime; 
        this.parent = parent;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(TimerSeq());
    }

    IEnumerator TimerSeq() {
        current = maxTime;

        while (current > 0) {
            current -= Time.deltaTime;
            fill.fillAmount = current / maxTime;
            yield return null; 
        }
        current = 0;
        fill.fillAmount = 0;
        if(parent != null)
            parent.OnTimerFinish(); 
    }
}



