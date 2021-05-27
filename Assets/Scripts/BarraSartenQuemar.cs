using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraSartenQuemar : MonoBehaviour
{
    Transform child;
    private bool isBorning;
    private Image timer;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("TimerSartenQuemar");
        timer = child.GetComponent<Image>();
        timer.enabled = false;
        isBorning = false;
    }

    public void RestarTiempo(float timeLeft, float maxTime)
    {
        if (!isBorning)
        {
            isBorning = true;
            timer.enabled = true;
        }

        timer.fillAmount = timeLeft / maxTime;

    }

    public void EndQuemar()
    {
        isBorning = false;
        timer.enabled = false;
    }
}
