using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SartenScript : MonoBehaviour
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
    public ParticleSystem ps;
    public Transform childp;

    // Start is called before the first frame update
    void Start()
    {
        GodModeNoQuemar = Canvas.GetComponent<Interfaz>().GodModeNoQuemar;
        GodModeEndCook = Canvas.GetComponent<Interfaz>().GodModeEndCook;
        timeToCook = TIMECOOK;
        timeToDelete = TIMEDELETE;
        if (this.gameObject.tag != "sartenEnded") numIngredients = 1;
        else
        {
            numIngredients = 0;
            allIngredients = true;
            if (!GodModeNoQuemar) timeToDelete = TIMEDELETE - Time.deltaTime;
            Canvas.GetComponent<BarraSartenQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
            timeToCook = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Canvas.GetComponent<Interfaz>().GodModeNoQuemar != GodModeNoQuemar) GodModeNoQuemar = Canvas.GetComponent<Interfaz>().GodModeNoQuemar;
        if (Canvas.GetComponent<Interfaz>().GodModeEndCook != GodModeEndCook) GodModeEndCook = Canvas.GetComponent<Interfaz>().GodModeEndCook;
        if (isCooking)
        {
           /// Debug.Log("timeToCook" + timeToCook);
            if (timeToCook == TIMECOOK)
            {
                timeToCook -= Time.deltaTime;
                Canvas.GetComponent<BarraSarten>().RestarTiempo(timeToCook, TIMECOOK);
            }
            else
            {
                if (timeToCook <= 0 || GodModeEndCook)
                {
                    //Debug.Log("timeToDelete" + timeToDelete);
                    Canvas.GetComponent<BarraSarten>().EndCook();
                    Debug.Log("allIngredients???" + allIngredients);
                    if (timeToDelete == TIMEDELETE && allIngredients)
                    {
                        //Debug.Log("S'HA DE CREAR ENDED");
                        string path = "Prefab/SartenEnded";
                        GameObject prefab = Resources.Load(path) as GameObject;
                        GameObject ObjectBefore = this.gameObject;


                        GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

                        tartaObject.GetComponent<PickableObject>().isPickable = true;
                        tartaObject.GetComponent<PickableObject>().isPicked = false;

                        tartaObject.transform.SetParent(null);
                        tartaObject.GetComponent<Rigidbody>().useGravity = true;
                        tartaObject.GetComponent<Rigidbody>().isKinematic = false;

                        tartaObject.GetComponent<SartenScript>().isCooking = true;
                        tartaObject.GetComponent<SartenScript>().timeToCook = 0;

                        childp = tartaObject.transform.Find("Particle System");
                        tartaObject.GetComponent<SartenScript>().ps = childp.GetComponent<ParticleSystem>();
                        this.isCooking = true;

                        tartaObject.GetComponent<SartenScript>().Canvas = Canvas;

                        tartaObject.tag = "sartenEnded";
                        /*tartaObject = null;
                        ObjectToPickUp = null;*/
                        if (!GodModeNoQuemar) timeToDelete -= Time.deltaTime;
                        Canvas.GetComponent<BarraSartenQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
                        Canvas.GetComponent<Interfaz>().GodModeEndCook = false;
                        Destroy(this.gameObject);
                    }
                    else if (timeToDelete > 0)
                    {
                        if (!GodModeNoQuemar) { 
                            //TODO: HAY QUE MIRAR SI LO TIENE EN LA MANO O NO, SI LO TIENE EN LA MANO NO SE QUEMA PORQUE NO ESTARIA EN EL FUEGO
                            timeToDelete -= Time.deltaTime;
                            Canvas.GetComponent<BarraSartenQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
                            Debug.Log("PSD?????? " + ps);
                            if (timeToDelete <= TIMEDELETE / 2)
                            {
                                var main = ps.main;
                                main.startColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                            }
                        }
                    }

                }
                else
                {
                    timeToCook -= Time.deltaTime;
                    Canvas.GetComponent<BarraSarten>().RestarTiempo(timeToCook, TIMECOOK);
                }
            }

            if (timeToDelete < TIMEDELETE)
            {

                if (timeToDelete <= 0)
                {
                    timeOut = true;
                    Canvas.GetComponent<BarraSartenQuemar>().EndQuemar();
                }
            }
        }

        if (timeOut)
        {

            timeToCook = TIMECOOK;
            timeToDelete = TIMEDELETE;
            //Destroy(la olla llena)
            GameObject ObjectBefore = this.gameObject;
            Destroy(this.gameObject);

            //se crea la olla prefab vacia
            string path = "Prefab/Sarten";
            GameObject prefab = Resources.Load(path) as GameObject;
            GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

            tartaObject.GetComponent<PickableObject>().isPickable = true;
            tartaObject.GetComponent<PickableObject>().isPicked = false;
            
            //this.isCooking = false;

            tartaObject.transform.SetParent(null);
            tartaObject.GetComponent<Rigidbody>().useGravity = true;
            tartaObject.GetComponent<Rigidbody>().isKinematic = false;

            tartaObject.GetComponent<SartenScript>().Canvas = Canvas;



        }
    }

    public void updateRecipe(bool isFirstTime)
    {

        if (numIngredients > 0)
        {
            if (isFirstTime)
            {

                //Cambiar animacion de no cocinar a cocinar, o eliminar el modelo y crear una olla nueva cocinando
                string path = "Prefab/SartenCocinando";
                GameObject prefab = Resources.Load(path) as GameObject;
                GameObject ObjectBefore = this.gameObject;
                //Destroy(this.gameObject);


                GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position , ObjectBefore.transform.rotation);

                tartaObject.GetComponent<PickableObject>().isPickable = true;
                tartaObject.GetComponent<PickableObject>().isPicked = false;

                tartaObject.transform.SetParent(null);
                tartaObject.GetComponent<Rigidbody>().useGravity = true;
                tartaObject.GetComponent<Rigidbody>().isKinematic = false;

                tartaObject.GetComponent<SartenScript>().isCooking = true;
                tartaObject.GetComponent<SartenScript>().allIngredients = true;

                
                tartaObject.GetComponent<SartenScript>().Canvas = Canvas;

                Destroy(this.gameObject);

                //tartaObject = null;
                //tartaObject.GetComponent<PickableObject>().ObjectToPickUp = tartaObject;
                //this.gameObject = tartaObject;

            }
            /*this.isCooking = true;
            --numIngredients;

            if (numIngredients == 0)
            {
                this.allIngredients = true;
            }
            Debug.Log("numIngredients" + numIngredients);
            Debug.Log("allIngredients" + allIngredients);*/
        }

    }


}


