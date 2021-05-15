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
    public bool hasToCut = false;

    private int counter = 420;
    
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

            if (Input.GetKeyDown(KeyCode.F) || (hasToCut && Input.GetKey(KeyCode.F) ))
            {                
                
                if (ObjectToPickUp != null  )
                {
                    Debug.Log("ObjectToPickUp " + ObjectToPickUp.tag + " counter " + counter);

                    //Si el objeto k tenemos delante es un knife i el objeto que tenemos encima es tipo food, que corte, sino la logica normal
                    if (ObjectToPickUp.tag == "knife" && PickedObject.tag == "food" && !PickedObject.GetComponent<PickableObject>().isCutted)
                    {
                        --counter;
                        Debug.Log("NO PLATO");
                        //Si hay que cortar lo indicamos
                        if (!hasToCut) hasToCut = true;

                        Debug.Log(counter);
                        //Cuando haya pasado el tiempo que hemos indicado, se para de indicar que estamos cortando, y se indica que el objeto ahora esta cortado
                        if (counter == 0)
                        {
                            Debug.Log("COUNTER 0");
                            hasToCut = false;
                            counter = 420;
                            PickedObject.GetComponent<PickableObject>().isCutted = true;
                            PickedObject.tag = "cuttedFood";
                        }
                    }
                    //Si la comida que llevas esta cortada y tienes un plato delante, dejas el alimento (Se elimina la comida y el alimento y se instancia un nuevo prefab plato lleno del tipo que sea) 
                    //TODO Hay que programar la parte de si es sartén o olla tambien!!
                    else if (ObjectToPickUp.tag == "plato"/* && counter == 420*/ )
                    {
                        if (PickedObject.tag == "cuttedFood") { 
                            
                            //Se tiene que cambiar por la creación de la nueva instancia con el prefab que sea
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
                        else if (PickedObject.GetComponent<PickableObject>().ObjectName.StartsWith("Tarta") )
                        {
                            string path = "Prefab/PlatoLlenoTarta";
                            GameObject prefab = Resources.Load(path) as GameObject;
                            GameObject ObjectBefore = ObjectToPickUp;

                            Destroy(PickedObject);
                            Destroy(ObjectToPickUp);

                            GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);

                            tartaObject.GetComponent<PickableObject>().isPickable = true;
                            tartaObject.GetComponent<PickableObject>().isPicked = false;

                            tartaObject.transform.SetParent(null);
                            tartaObject.GetComponent<Rigidbody>().useGravity = true;
                            tartaObject.GetComponent<Rigidbody>().isKinematic = false;
                            tartaObject = null;
                            ObjectToPickUp = null;
                            animator.SetBool("Carry", false);

                        }
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
