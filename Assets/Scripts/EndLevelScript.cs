using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    private Text puntos, level;
    private Image congrats, fail;
    Transform child;
    public float time = 4;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("Puntuacion");
        puntos = child.GetComponent<Text>();

        child = transform.Find("Nivel");
        level = child.GetComponent<Text>();

        //Images
        child = transform.Find("Congrats");
        congrats = child.GetComponent<Image>();
        congrats.enabled = false;

        child = transform.Find("Fail");
        fail = child.GetComponent<Image>();
        fail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        int aux = StaticScenes.numEscena - 1;
        bool minpunt = (StaticScenes.puntuacion > 100);

        if (minpunt)congrats.enabled = true;        
        else fail.enabled = true;

        puntos.text = StaticScenes.puntuacion.ToString();
        level.text = aux.ToString();
        //Si no llega al min de puntos que no pase, vaya atras

        if (time <= 0) {

            if (minpunt)
            {
                fail.enabled = false;
                //Esperar 5sec antes de esto
                if (Input.anyKeyDown)
                {
                    if (StaticScenes.numEscena == 2)
                    {//Si es escena 1
                        SceneManager.LoadScene(sceneName: "SampleScene2");
                    }
                    else if (StaticScenes.numEscena == 3)
                    {//Si es escena 1
                        SceneManager.LoadScene(sceneName: "SampleScene3");
                    }
                    else if (StaticScenes.numEscena == 4)
                    {//Si es escena 1
                        SceneManager.LoadScene(sceneName: "SimpleScene4");
                    }
                    else if (StaticScenes.numEscena == 5)
                    {//Si es escena 1
                        SceneManager.LoadScene(sceneName: "SampleScene5");
                    }
                    else
                    {
                        //Game over
                       

                    }
                }
            }
            else
            {//No ha obtenido puntos suficientes
                if (StaticScenes.numEscena == 2)
                {//Si es escena 1
                    SceneManager.LoadScene(sceneName: "SampleScene");
                    StaticScenes.numEscena = 1;
                }
                else if (StaticScenes.numEscena == 3)
                {//Si es escena 1
                    SceneManager.LoadScene(sceneName: "SampleScene2");
                    StaticScenes.numEscena = 2;
                }
                else if (StaticScenes.numEscena == 4)
                {//Si es escena 1
                    SceneManager.LoadScene(sceneName: "SimpleScene3");
                    StaticScenes.numEscena = 3;
                }
                else if (StaticScenes.numEscena == 5)
                {//Si es escena 1
                    SceneManager.LoadScene(sceneName: "SampleScene4");
                    StaticScenes.numEscena = 4;
                }
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
