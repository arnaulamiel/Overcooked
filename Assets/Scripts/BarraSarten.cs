using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraSarten : MonoBehaviour
{
    Transform child;
    private bool isCooking;
    private Image timer;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("TimerSarten");
        timer = child.GetComponent<Image>();
        timer.enabled = false;
        isCooking = false;
    }


    public void RestarTiempo(float timeLeft, float maxTime)
    {
        if (!isCooking)
        {
            isCooking = true;
            timer.enabled = true;
        }

        timer.fillAmount = timeLeft / maxTime;

    }

    public void EndCook()
    {
        isCooking = false;
        timer.enabled = false;
    }
}
