using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOlla : MonoBehaviour
{
    public AudioClip OllaSound;

    AudioSource fuenteAudio;

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }

    public void isCooking()
    {
        fuenteAudio.clip = OllaSound;
        fuenteAudio.Play();
    }

    public void isEmpty()
    {
        fuenteAudio.clip = null;
    }
}
