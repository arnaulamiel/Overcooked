using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScrip : MonoBehaviour
{
    private Image press;
    Transform child;
    public float time;

    public float intermitente = 1;

    // Start is called before the first frame update
    void Start()
    {
        time = 4;
        child = transform.Find("GoMenu");
        press = child.GetComponent<Image>();
        press.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(sceneName: "MenuScene");
                StaticScenes.numEscena = 0;
            }

            if (intermitente <= 0) {
                if (press.enabled) press.enabled = false;
                else press.enabled = true;
                intermitente = 1;
            }
            else
            {
                intermitente -= Time.deltaTime;
                if (press.enabled) press.enabled = true;
                else press.enabled = false;
            }



        }
        else {
            time -= Time.deltaTime;
        }
    }
}
