using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllaScript : MonoBehaviour
{

    public bool timeOut = false;
    public bool isCooking = false;
    public int timeToCook;
    private int timeToDelete;
    public bool allIngredients = false;
    private int numIngredients;
    
    public GameObject player;

    private const int TIMECOOK = 4500;
    private const int TIMEDELETE = 1200;
    /* enum State
     {
         NoCooking,
         Cooking,
         Burning
     }
    */
    // Start is called before the first frame update
    void Start()
    {
        //Ahora solo se hace general, igual necesitamos especificar, en frames
        timeToCook = TIMECOOK;
        timeToDelete = TIMEDELETE;
        if (this.gameObject.tag != "ollaEnded") numIngredients = 2;
        else { 
            numIngredients = 0;
            allIngredients = true;
            timeToDelete = TIMEDELETE -1;
            timeToCook = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isCooking) {
           
            if (timeToCook == TIMECOOK)
            {
                --timeToCook;
            }
            else {                
                if(timeToCook == 0)
                {
                   
                    if (timeToDelete == TIMEDELETE && allIngredients)
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

                        tartaObject.tag = "ollaEnded";
                        /*tartaObject = null;
                        ObjectToPickUp = null;*/
                        --timeToDelete;
                        Destroy(this.gameObject);
                    }
                    else if (timeToDelete != 0) {
                        //TODO: HAY QUE MIRAR SI LO TIENE EN LA MANO O NO, SI LO TIENE EN LA MANO NO SE QUEMA PORQUE NO ESTARIA EN EL FUEGO
                        --timeToDelete;
                    }

                }
                else {
                    --timeToCook; 
                }
            }

            if (timeToDelete != TIMEDELETE)
            {
               
                if (timeToDelete == 0)timeOut = true; 
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
    
    
}
