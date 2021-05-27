using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    public AudioClip cutSound;
    public AudioClip interaction;

    AudioSource fuenteAudio;

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void isCutting()
    {
        fuenteAudio.clip = cutSound;
        fuenteAudio.loop = true;
        fuenteAudio.Play();
    }

    public void stopCutting()
    {
        fuenteAudio.clip = null;
        fuenteAudio.loop = false;
    }

    public void interactionSound()
    {
        fuenteAudio.clip = interaction;
        fuenteAudio.Play();
    }
}
