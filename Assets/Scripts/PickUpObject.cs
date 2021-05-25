using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public GameObject Hand;
    public GameObject ObjectCinta;
    public Transform interactionZone;
    public Animator animator;
    public Transform player;
    public bool hasToCut = false;

    public int puntuacion = 0;
    private int counter = 420;
    public int numCebollasParaCompletar;
    public int numEnsaladas = 0;
    public int numSopas = 0;

    private GameObject cuchillo;
    void Start()
    {
        numCebollasParaCompletar = 3;
    }
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
                   // Debug.Log("ObjectToPickUp " + ObjectToPickUp.tag + " counter " + counter);

                    //Si el objeto k tenemos delante es una tabla de cortar i el objeto que tenemos encima es tipo food, que corte, sino la logica normal
                    if (ObjectToPickUp.tag == "tablaCortar" && PickedObject.tag == "food" && !PickedObject.GetComponent<PickableObject>().isCutted)
                    {
                        --counter;
                       // Debug.Log("NO PLATO");
                        //Si hay que cortar lo indicamos
                        if (!hasToCut)
                        {
                            hasToCut = true;
                            PickedObject.transform.SetParent(ObjectToPickUp.transform);
                            PickedObject.transform.position = ObjectToPickUp.transform.position;
                            animator.SetBool("isCutting", true);

                            //Coger cuchillo
                            cuchillo = ObjectToPickUp.GetComponent<TablaCortar>().cuchillo;
                            cuchillo.transform.SetParent(Hand.transform);
                            cuchillo.transform.position = Hand.transform.position;
                            cuchillo.transform.rotation = Hand.transform.rotation;
                            cuchillo.transform.Rotate(26.38f, -71.74f, 16.24f, Space.Self);
                            cuchillo.transform.Translate(0.0204f, 0.0027f, 0.0005f);

                        }
                        Debug.Log(counter);
                        //Cuando haya pasado el tiempo que hemos indicado, se para de indicar que estamos cortando, y se indica que el objeto ahora esta cortado
                        if (counter == 0)
                        {
                            //Debug.Log("COUNTER 0");
                            hasToCut = false;
                            counter = 420;
                            string path = "";
                            if (PickedObject.GetComponent<PickableObject>().ObjectName.Contains("Tomate"))
                            {
                                path = "Prefab/TomateCortado";
                                GameObject prefab = Resources.Load(path) as GameObject;
                                GameObject ObjectBefore = PickedObject;
                                Destroy(PickedObject);

                                //GameObject tartaObject = GameObject.Instantiate(prefab, ObjectBefore.transform.position, ObjectBefore.transform.rotation);


                                GameObject tomatecortado = GameObject.Instantiate(prefab);


                                PickedObject = tomatecortado;
                            }
                            PickedObject.GetComponent<PickableObject>().isCutted = true;
                            PickedObject.GetComponent<PickableObject>().isPickable = false;
                            PickedObject.GetComponent<PickableObject>().isPicked = true;
                            PickedObject.tag = "cuttedFood";
                            PickedObject.transform.SetParent(interactionZone);
                            PickedObject.GetComponent<Rigidbody>().useGravity = false;
                            PickedObject.GetComponent<Rigidbody>().isKinematic = true;
                            PickedObject.transform.position = interactionZone.position;
                            PickedObject.transform.rotation = player.rotation;
                            PickedObject.transform.Translate(-0.1f, 2f, -0.5f);
                            animator.SetBool("isCutting", false);

                            //dejar cuchillo
                            cuchillo = ObjectToPickUp.GetComponent<TablaCortar>().cuchillo;
                            cuchillo.transform.SetParent(ObjectToPickUp.transform);
                            cuchillo.transform.position = ObjectToPickUp.transform.position;
                            cuchillo.transform.rotation = ObjectToPickUp.transform.rotation;
                            cuchillo.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                            cuchillo.transform.Translate(0.2096001f, 0.1074545f, 0.02920001f);
                        }
                    }
                    //Si la comida que llevas esta cortada y tienes un plato delante, dejas el alimento (Se elimina la comida y el alimento y se instancia un nuevo prefab plato lleno del tipo que sea) 
                    //TODO Hay que programar la parte de si es sartén o olla tambien!!
                    else if (ObjectToPickUp.tag == "plato"/* && counter == 420*/ )
                    {
                        if (PickedObject.tag == "cuttedFood") {

                            //Receta Ensalada
                            if (PickedObject.GetComponent<PickableObject>().ObjectName.StartsWith("Repollo") || PickedObject.GetComponent<PickableObject>().ObjectName.StartsWith("Tomate")){
                                //Debug.Log("Reconoce tomate o lechuga");
                                if (PickedObject.GetComponent<PickableObject>().ObjectName.StartsWith("Repollo"))
                                {
                                    //Debug.Log("Reconoce lechuga");
                                    if (!ObjectToPickUp.GetComponent<PickableObject>().ObjectName.StartsWith("PlatoLleno")) { 
                                        string path = "Prefab/PlatoLlenoEnsaladaL";
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
                                    }//Si donde queremos poner el repollo es un plato ya lleno pero de tomate, creamos la ensalada
                                    else if(ObjectToPickUp.GetComponent<PickableObject>().ObjectName.StartsWith("PlatoLlenoEnsaladaT"))
                                    {
                                        string path = "Prefab/PlatoLlenoEnsalada";
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
                                        tartaObject.GetComponent<PickableObject>().isEnded = true;
                                        tartaObject = null;
                                        ObjectToPickUp = null;
                                        animator.SetBool("Carry", false);
                                        
                                    }
                                }
                                else
                                {
                                    Debug.Log("Reconoce toamte");
                                    if (!ObjectToPickUp.GetComponent<PickableObject>().ObjectName.StartsWith("PlatoLleno"))
                                    {
                                        
                                        string path = "Prefab/PlatoLlenoEnsaladaT";
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
                                    }//Si donde queremos poner el repollo es un plato ya lleno pero de tomate, creamos la ensalada
                                    else if (ObjectToPickUp.GetComponent<PickableObject>().ObjectName.StartsWith("PlatoLlenoEnsaladaL"))
                                    {
                                        string path = "Prefab/PlatoLlenoEnsalada";
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
                                        Debug.Log("IS ENDED IS ENDED IS ENDED");
                                        tartaObject.GetComponent<PickableObject>().isEnded = true;
                                        tartaObject = null;
                                        ObjectToPickUp = null;
                                        animator.SetBool("Carry", false);
                                        
                                    }
                                }
                                

                            }
                            //Se tiene que cambiar por la creación de la nueva instancia con el prefab que sea
                            /*PickedObject.GetComponent<PickableObject>().isPickable = true;
                            PickedObject.GetComponent<PickableObject>().isPicked = false;
                            PickedObject.transform.Translate(0.0f, -0.5f, 1.6f);
                            PickedObject.transform.SetParent(null);
                            PickedObject.GetComponent<Rigidbody>().useGravity = true;
                            PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                            PickedObject = null;
                            ObjectToPickUp = null;
                            animator.SetBool("Carry", false);*/

                        }
                        else if(PickedObject.tag == "ollaEnded")
                        {
                            string path = "Prefab/PlatoLlenoSopa";
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
                            tartaObject.GetComponent<PickableObject>().isEnded = true;
                            tartaObject = null;
                            ObjectToPickUp = null;

                            string path2 = "Prefab/Olla";
                            GameObject prefab2 = Resources.Load(path2) as GameObject;
                            GameObject olla = GameObject.Instantiate(prefab2);


                            PickedObject = olla;
                            PickedObject.GetComponent<PickableObject>().isPickable = false;
                            PickedObject.GetComponent<PickableObject>().isPicked = true;
                            PickedObject.transform.SetParent(interactionZone);
                            PickedObject.GetComponent<Rigidbody>().useGravity = false;
                            PickedObject.GetComponent<Rigidbody>().isKinematic = true;
                            PickedObject.transform.position = interactionZone.position;
                            PickedObject.transform.rotation = player.rotation;
                            PickedObject.transform.Translate(-0.1f, 2f, -0.5f);
                            animator.SetBool("Carry", true);


                        }
                        //Esto era para probar la funcionalidad, la tarta no creo que la pongamos 
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
                    else if(ObjectToPickUp.tag == "olla")
                    {
                        
                        if (PickedObject.tag == "cuttedFood")
                        {
                           
                            if (PickedObject.GetComponent<PickableObject>().ObjectName.StartsWith("Cebolla"))
                            {
                                bool firstTime = ObjectToPickUp.GetComponent<OllaScript>().isCooking;
                                
                                Debug.Log("Cooking?????? " + ObjectToPickUp.GetComponent<OllaScript>().isCooking);
                                if (!ObjectToPickUp.GetComponent<OllaScript>().allIngredients)
                                {
                                    Destroy(PickedObject);
                                    animator.SetBool("Carry", false);
                                }
                                ObjectToPickUp.GetComponent<OllaScript>().updateRecipe(!firstTime);
                            }

                        }
                    }
                    
                }
                //si no tienes un objeto delante pero llevas un plato acabado
                else if (PickedObject.GetComponent<PickableObject>().isEnded && ObjectCinta != null)
                {
                    if (PickedObject.GetComponent<PickableObject>().ObjectName.Contains("Ensalada")) {
                        puntuacion += 10;
                        ++numEnsaladas;
                       
                    }
                    if (PickedObject.GetComponent<PickableObject>().ObjectName.Contains("Sopa"))
                    {
                        puntuacion += 30;
                        ++numSopas;

                    }
                    Destroy(PickedObject);
                    PickedObject = null;
                    ObjectToPickUp = null;
                    animator.SetBool("Carry", false);
                    

                    
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
