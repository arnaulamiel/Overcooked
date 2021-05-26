using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraOlla : MonoBehaviour
{
    public GameObject olla;
    Transform child;
    private bool isCooking;
    private float maxTime;
    private float timeLeft;
    private Image timer;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("TimerOlla");
        timer = child.GetComponent<Image>();
        timer.enabled = false;
        isCooking = olla.GetComponent<OllaScript>().isCooking;
        maxTime = olla.GetComponent<OllaScript>().TIMECOOK;
    }

    // Update is called once per frame
    void Update()
    {
        isCooking = olla.GetComponent<OllaScript>().isCooking;
        if (isCooking)
        {
            timer.enabled = true;
            timeLeft = olla.GetComponent<OllaScript>().timeToCook;
            timer.fillAmount = timeLeft / maxTime;
        }
        else timer.enabled = false;
    }
}
