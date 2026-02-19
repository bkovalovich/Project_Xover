using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image fill;
    ProtectObjestive_ScenarioManager parent;
    private float maxTime;
    private float time;

    public void Awake()
    {
        parent = GetComponent<ProtectObjestive_ScenarioManager>();
        maxTime = parent.GetDefendTime();
        time = maxTime;
    }

    private void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        fill.fillAmount = time / maxTime;

        if (time <= 0)
        {
            time = 0;
            fill.fillAmount = 0;
            parent.VictoryCheck(true);
        }
    }
}
