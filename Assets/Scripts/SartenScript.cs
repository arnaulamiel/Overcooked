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

    public float TIMECOOK = 15f;
    public float TIMEDELETE = 10f;
    // Start is called before the first frame update
    void Start()
    {
        timeToCook = TIMECOOK;
        timeToDelete = TIMEDELETE;
        if (this.gameObject.tag != "sartenEnded") numIngredients = 1;
        else
        {
            numIngredients = 0;
            allIngredients = true;
            timeToDelete = TIMEDELETE - Time.deltaTime;
            Canvas.GetComponent<BarraSartenQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
            timeToCook = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                if (timeToCook <= 0)
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

                        this.isCooking = true;

                        tartaObject.GetComponent<SartenScript>().Canvas = Canvas;

                        tartaObject.tag = "sartenEnded";
                        /*tartaObject = null;
                        ObjectToPickUp = null;*/
                        timeToDelete -= Time.deltaTime;
                        Canvas.GetComponent<BarraSartenQuemar>().RestarTiempo(timeToDelete, TIMEDELETE); 
                        Destroy(this.gameObject);
                    }
                    else if (timeToDelete > 0)
                    {
                        //TODO: HAY QUE MIRAR SI LO TIENE EN LA MANO O NO, SI LO TIENE EN LA MANO NO SE QUEMA PORQUE NO ESTARIA EN EL FUEGO
                        timeToDelete -= Time.deltaTime;
                        Canvas.GetComponent<BarraSartenQuemar>().RestarTiempo(timeToDelete, TIMEDELETE);
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
            this.isCooking = false;

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


