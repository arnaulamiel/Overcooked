using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    private Text puntos, level;
    Transform child;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("Puntuacion");
        puntos = child.GetComponent<Text>();

        child = transform.Find("Nivel");
        level = child.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int aux = StaticScenes.numEscena - 1;
        puntos.text = StaticScenes.puntuacion.ToString();
        //Si no llega al min de puntos que no pase, vaya atras
        
        level.text = aux.ToString();

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
            else { 
                //Game over
                
            }
        }
    }
}
