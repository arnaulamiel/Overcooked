using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsSarten : MonoBehaviour
{
    public AudioClip SartenSound;

    AudioSource fuenteAudio;

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }

    public void isCooking()
    {
        fuenteAudio.clip = SartenSound;
        fuenteAudio.Play();
    }

    public void isEmpty()
    {
        fuenteAudio.clip = null;
    }
}
