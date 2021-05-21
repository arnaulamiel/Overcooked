using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CintaServir : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectCinta = this.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectCinta = null;

        }
    }
}
