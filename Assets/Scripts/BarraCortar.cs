using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraCortar : MonoBehaviour
{

    public GameObject player;
    Transform child;
    private bool isCutting;
    private float maxTime;
    private float timeLeft;
    private Image timer;
    
    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("TimerCut");
        timer = child.GetComponent<Image>();
        timer.enabled = false;
        isCutting = player.GetComponent<PickUpObject>().hasToCut;
        maxTime = player.GetComponent<PickUpObject>().maxTimeCut;
    }

    // Update is called once per frame
    void Update()
    {
        isCutting = player.GetComponent<PickUpObject>().hasToCut;
        if (isCutting)
        {
            timer.enabled = true;
            timeLeft = player.GetComponent<PickUpObject>().timeLeftCut;
            timer.fillAmount = timeLeft / maxTime;
        }
        else timer.enabled = false;
    }
}
