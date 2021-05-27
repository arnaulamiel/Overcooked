using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllaScript : MonoBehaviour
{

    public bool timeOut = false;
    public bool isCooking = false;
    public float timeToCook;
    private float timeToDelete;
    public bool allIngredients = false;
    private int numIngredients;
    
    public GameObject player;
    public GameObject Canvas;

    private bool GodModeNoQuemar = false;
    private bool GodModeEndCook = false;

    public float TIMECOOK = 15f;
    public float TIMEDELETE = 10f;
    public enum TipoComida
     {
        Nada,
        Cebolla,
        Tomate,
        Zanahoria
         
     }
    public TipoComida comida;
    public ParticleSystem ps;
    public Transform childp;

    public void setComida(TipoComida tipo)
    {
        this.comida = tipo;
    }

    public AudioClip OllaSound;

    AudioSource fuenteAudio;

    // Start is called before the first frame update
    void Start()
    {
        GodModeNoQuemar = Canvas.GetComponent<Interfaz>().GodModeNoQuemar;
        GodModeEndCook = Canvas.GetComponent<Interfaz>().GodModeEndCook;
        fuenteAudio = GetComponent<AudioSource>();
        //Ahora solo se hace general, igual necesitamos especificar, en frames
        timeToCook = TIMECOOK;
        timeToDelete = TIMEDELETE;
        if (this.gameObject.tag != "ollaEnded") numIngredients = 2;
        else { 
            numIngredients = 0;
            allIngredients = true;
            if(!GodModeNoQuemar) timeToDelete = TIMEDELETE - Time.deltaTime;
            Canvas.GetComponent<BarraOllaQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
            timeToCook = 0;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Canvas.GetComponent<Interfaz>().GodModeNoQuemar != GodModeNoQuemar ) GodModeNoQuemar = Canvas.GetComponent<Interfaz>().GodModeNoQuemar;
        if (Canvas.GetComponent<Interfaz>().GodModeEndCook != GodModeEndCook) GodModeEndCook = Canvas.GetComponent<Interfaz>().GodModeEndCook;
        if (isCooking)
            if (isCooking) {
           
            if (timeToCook == TIMECOOK)
            {
                fuenteAudio.clip = OllaSound;
                fuenteAudio.Play();
                timeToCook -= Time.deltaTime;
                Canvas.GetComponent<BarraOlla>().RestarTiempo(timeToCook, TIMECOOK);
            }
            else {                
                if(timeToCook <= 0 || GodModeEndCook)
                {
                    Canvas.GetComponent<BarraOlla>().EndCook();
                    if ((timeToDelete == TIMEDELETE && allIngredients) || GodModeEndCook)
                    {
                        string path = "Prefab/OllaEnded";
                        GameObject prefab = Resources.Load(path) as GameObject;
                        GameObject ObjectBefore = this.gameObject;


                        GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

                        tartaObject.GetComponent<PickableObject>().isPickable = true;
                        tartaObject.GetComponent<PickableObject>().isPicked = false;

                        tartaObject.transform.SetParent(null);
                        tartaObject.GetComponent<Rigidbody>().useGravity = true;
                        tartaObject.GetComponent<Rigidbody>().isKinematic = false;

                        tartaObject.GetComponent<OllaScript>().isCooking = true;

                        tartaObject.GetComponent<OllaScript>().setComida(this.comida);

                        tartaObject.GetComponent<OllaScript>().Canvas = Canvas;

                        tartaObject.GetComponent<OllaScript>().OllaSound = OllaSound;

                        tartaObject.tag = "ollaEnded";

                        /*tartaObject = null;
                        ObjectToPickUp = null;*/
                        if(!GodModeNoQuemar) timeToDelete -= Time.deltaTime;
                        Canvas.GetComponent<BarraOllaQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
                            Debug.Log("ENDCOOOOOK "+ GodModeEndCook);
                        if (GodModeEndCook)
                        {
                            Canvas.GetComponent<BarraOllaQuemar>().EndQuemar();
                            Canvas.GetComponent<Interfaz>().GodModeEndCook = false;
                        }
                        Destroy(this.gameObject);
                    }
                    else if (timeToDelete > 0) {
                        //TODO: HAY QUE MIRAR SI LO TIENE EN LA MANO O NO, SI LO TIENE EN LA MANO NO SE QUEMA PORQUE NO ESTARIA EN EL FUEGO

                        if (!GodModeNoQuemar)
                        {
                            timeToDelete -= Time.deltaTime;
                            Canvas.GetComponent<BarraOllaQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
                            var main = ps.main;
                            main.startColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                        }

                    }

                }
                else {
                    timeToCook -= Time.deltaTime;
                    Canvas.GetComponent<BarraOlla>().RestarTiempo(timeToCook, TIMECOOK);
                }
            }

            if (timeToDelete < TIMEDELETE)
            {

                if (timeToDelete <= 0)
                {
                    fuenteAudio.clip = null;
                    timeOut = true;
                    Canvas.GetComponent<BarraOllaQuemar>().EndQuemar();
                }
            }
        }
        
        //Si se quema
        if (timeOut)
        {
            
            timeToCook = TIMECOOK;
            timeToDelete = TIMEDELETE;
            //Destroy(la olla llena)
            GameObject ObjectBefore = this.gameObject;
            Destroy(this.gameObject);

            //se crea la olla prefab vacia
            string path = "Prefab/Olla";
            GameObject prefab = Resources.Load(path) as GameObject;
            GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

            tartaObject.GetComponent<PickableObject>().isPickable = true;
            tartaObject.GetComponent<PickableObject>().isPicked = false;
            tartaObject.GetComponent<OllaScript>().isCooking = false;

            tartaObject.transform.SetParent(null);
            tartaObject.GetComponent<Rigidbody>().useGravity = true;
            tartaObject.GetComponent<Rigidbody>().isKinematic = false;


            tartaObject.GetComponent<OllaScript>().Canvas = Canvas;

            tartaObject.GetComponent<OllaScript>().OllaSound = OllaSound;

        }
    }

    public void updateRecipe(bool isFirstTime)
    {
       
        if (numIngredients > 0)
        {
            if (isFirstTime)
            {
                
                //Cambiar animacion de no cocinar a cocinar, o eliminar el modelo y crear una olla nueva cocinando
                string path = "Prefab/OllaBoiling";
                GameObject prefab = Resources.Load(path) as GameObject;
                GameObject ObjectBefore = this.gameObject;
                //Destroy(this.gameObject);


                GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

                tartaObject.GetComponent<PickableObject>().isPickable = true;
                tartaObject.GetComponent<PickableObject>().isPicked = false;

                tartaObject.transform.SetParent(null);
                tartaObject.GetComponent<Rigidbody>().useGravity = true;
                tartaObject.GetComponent<Rigidbody>().isKinematic = false;

                tartaObject.GetComponent<OllaScript>().isCooking = true;
                tartaObject.GetComponent<OllaScript>().setComida(this.comida);
                childp = tartaObject.transform.Find("Particle System");
                tartaObject.GetComponent<OllaScript>().ps = childp.GetComponent<ParticleSystem>();
               
                Debug.Log("Boiling " + tartaObject.GetComponent<OllaScript>().ps);
                tartaObject.GetComponent<OllaScript>().Canvas = Canvas;

                tartaObject.GetComponent<OllaScript>().OllaSound = OllaSound;

                Destroy(this.gameObject);

                //tartaObject = null;
                //tartaObject.GetComponent<PickableObject>().ObjectToPickUp = tartaObject;
                //this.gameObject = tartaObject;

            }
            isCooking = true;
            --numIngredients;

            if (numIngredients == 0)
            {
                allIngredients = true;
            }
            Debug.Log("numIngredients" + numIngredients);
        }
        
    }

    public bool isSameType(string type)
    {
        Debug.Log("Es tipo: " + type);
        if(this.comida != TipoComida.Nada)
        {
            Debug.Log("Debe ser nulo " + this.comida);
            if (type == "Cebolla" && this.comida != TipoComida.Cebolla) return false;
            else if (type == "Tomate" && this.comida != TipoComida.Tomate) return false;
            else if (type == "Zanahoria" && this.comida != TipoComida.Zanahoria) return false;
        }
        return true;
    }




}
