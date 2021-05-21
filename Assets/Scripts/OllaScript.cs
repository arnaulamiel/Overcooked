using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllaScript : MonoBehaviour
{

    public bool timeOut = false;
    public bool isCooking = false;
    private int timeToCook;
    private int timeToDelete;
    public GameObject player;
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
        timeToCook = 1600;
        timeToDelete = 180;

    }

    // Update is called once per frame
    void Update()
    {
        if (isCooking) { 
            if(timeToCook == 1600)
            {
                --timeToCook;
            }
            else {                
                if(timeToCook == 0)
                {
                    --timeToDelete;
                    
                }else {
                    --timeToCook; 
                }
            }

            if (timeToDelete != 180)
            {
                if (timeToDelete != 0) --timeToDelete;
                else timeOut = true;
            }
        }
        
        //Si se quema
        if (timeOut)
        {
            timeToCook = 1600;
            timeToDelete = 180;
            player.GetComponent<PickUpObject>().numCebollasParaCompletar = 3;
            Destroy(this.gameObject);
            //Destroy(la olla llena)
            //se crea la olla prefab vacia
        }
    }
}
