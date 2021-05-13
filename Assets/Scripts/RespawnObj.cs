using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObj : MonoBehaviour
{
    public bool isRespawned = true;
    private Vector3 originalPos;
    private Quaternion originalRot;

    void Start()
    {
        originalPos = gameObject.transform.position;
        originalRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {       

        if (gameObject.GetComponent<PickableObject>().isPicked && this.isRespawned)
        {
            
            GameObject newObject = Instantiate(gameObject, originalPos, originalRot);
            newObject.GetComponent<PickableObject>().isPicked = false;
            newObject.GetComponent<PickableObject>().isPickable = true;
            newObject.GetComponent<RespawnObj>().isRespawned = true;
            this.isRespawned = false;
        }
        
    }
}
