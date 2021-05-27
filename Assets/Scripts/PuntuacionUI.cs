using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionUI : MonoBehaviour
{
    //public Text puntuacion;
    public GameObject player;
    Text puntuacion;
    Transform child;
    Text puntos;
    float puntosTime = 1.5f;
    int prevPuntos = 0;

    void Start()
    {
        child = transform.Find("Puntuacion");
        puntuacion = child.GetComponent<Text>();
        //Debug.Log("puntuacion???? " + puntuacion + " Get>Text " + gameObject.GetComponent<Text>() );
        //puntuacion.text = "Puntuación : " + player.GetComponent<PickUpObject>().puntuacion;
        puntuacion.text = "Puntuación : " + gameObject.GetComponent<Interfaz>().puntuacion;

        child = transform.Find("Text");
        puntos = child.GetComponent<Text>();

        puntos.enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
        int puntosAct = gameObject.GetComponent<Interfaz>().puntuacion;
        puntuacion.text = "Puntuación : " + puntosAct;
        Debug.Log("PUNTUACION ANTES" + puntosAct);
        
        
        Debug.Log("PUNTUACION NOW" + prevPuntos);

        if (puntosAct > prevPuntos) {
            Debug.Log("Actualizado " + (puntosAct - prevPuntos) );
            puntos.enabled = true;
            puntos.text = "+" + (puntosAct - prevPuntos);
            puntosTime -= Time.deltaTime;
        }
        
        if (puntosTime <= 0) {
            puntos.enabled = false;
            puntosTime = 1.5f;
        }
        else if(puntosTime > 0 && puntosTime < 1.5f)
        {
            //if(puntos)
            puntosTime -= Time.deltaTime;
        }
        prevPuntos = gameObject.GetComponent<Interfaz>().puntuacion;
    }
}
