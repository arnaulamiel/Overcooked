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

    void Start()
    {
        child = transform.Find("Puntuacion");
        puntuacion = child.GetComponent<Text>();
        //Debug.Log("puntuacion???? " + puntuacion + " Get>Text " + gameObject.GetComponent<Text>() );
        puntuacion.text = "Puntuación : " + player.GetComponent<PickUpObject>().puntuacion;  
    }
    // Update is called once per frame
    void Update()
    {
        puntuacion.text = "Puntuación : "+ player.GetComponent<PickUpObject>().puntuacion;

    }
}
