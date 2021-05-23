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
    public GameObject player;

    private const int TIMECOOK = 2500;
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

    }

    // Update is called once per frame
    void Update()
    {
        if (isCooking) {
            //Debug.Log("timeToDelete??: " + timeToDelete);
            if(timeToCook == TIMECOOK)
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
                        Destroy(this.gameObject); 

                        GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

                        tartaObject.GetComponent<PickableObject>().isPickable = true;
                        tartaObject.GetComponent<PickableObject>().isPicked = false;

                        tartaObject.transform.SetParent(null);
                        tartaObject.GetComponent<Rigidbody>().useGravity = true;
                        tartaObject.GetComponent<Rigidbody>().isKinematic = false;
                        tartaObject.tag = "ollaEnded";
                        /*tartaObject = null;
                        ObjectToPickUp = null;*/
                        --timeToDelete;
                        allIngredients = false;
                    }
                    else if (timeToDelete != 0) --timeToDelete;

                }
                else {
                    --timeToCook; 
                }
            }

            if (timeToDelete != TIMEDELETE)
            {
                if (timeToDelete != 0) --timeToDelete;
                else timeOut = true;
            }
        }
        
        //Si se quema
        if (timeOut)
        {
            timeToCook = TIMECOOK;
            timeToDelete = TIMEDELETE;
            player.GetComponent<PickUpObject>().numCebollasParaCompletar = 3;
            //Destroy(la olla llena)
            GameObject ObjectBefore = this.gameObject;
            Destroy(this.gameObject);

            //se crea la olla prefab vacia
            string path = "Prefab/Olla";
            GameObject prefab = Resources.Load(path) as GameObject;
            GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

            tartaObject.GetComponent<PickableObject>().isPickable = true;
            tartaObject.GetComponent<PickableObject>().isPicked = false;

            tartaObject.transform.SetParent(null);
            tartaObject.GetComponent<Rigidbody>().useGravity = true;
            tartaObject.GetComponent<Rigidbody>().isKinematic = false;



        }
    }
}
