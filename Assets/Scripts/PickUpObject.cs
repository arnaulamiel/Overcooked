using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;
    public Animator animator;
    public Transform player;
    
    void Update()
    {
        
        if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObject>().isPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickedObject = ObjectToPickUp;
                PickedObject.GetComponent<PickableObject>().isPickable = false;
                PickedObject.GetComponent<PickableObject>().isPicked = true;
                PickedObject.transform.SetParent(interactionZone);
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
                PickedObject.transform.position = interactionZone.position;
                PickedObject.transform.rotation = player.rotation;
                PickedObject.transform.Translate(-0.1f, 2f, -0.5f);
                animator.SetBool("Carry", true);
                ObjectToPickUp = null;
            }
        }
        else if (PickedObject != null)
        {
            


            if (Input.GetKeyDown(KeyCode.F))
            {
                
                //Si el objeto k tenemos delante es un knife i el objeto que tenemos encima es tipo food, que corte (animacion y eso), sino la logica normal
                if (ObjectToPickUp != null )
                {
                    if (ObjectToPickUp.tag == "knife" && PickedObject.tag == "food"  )
                    {
                        Debug.Log("Entra");
                    }
                    else if(ObjectToPickUp.tag == "plato")
                    {
                        PickedObject.GetComponent<PickableObject>().isPickable = true;
                        PickedObject.GetComponent<PickableObject>().isPicked = false;
                        PickedObject.transform.Translate(0.0f, -0.5f, 1.6f);
                        PickedObject.transform.SetParent(null);
                        PickedObject.GetComponent<Rigidbody>().useGravity = true;
                        PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                        PickedObject = null;
                        ObjectToPickUp = null;
                        animator.SetBool("Carry", false);
                    }
                }
                else {
                    PickedObject.GetComponent<PickableObject>().isPickable = true;
                    PickedObject.GetComponent<PickableObject>().isPicked = false;
                    PickedObject.transform.Translate(0.0f, -0.5f, 1.6f);
                    PickedObject.transform.SetParent(null);
                    PickedObject.GetComponent<Rigidbody>().useGravity = true;
                    PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                    PickedObject = null;
                    ObjectToPickUp = null;
                    animator.SetBool("Carry", false); 
                }
            }
        }
    }
}
