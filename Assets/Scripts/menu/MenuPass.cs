using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuPass : MonoBehaviour
{
    private int pasos = 0;
    public Image image;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        string path = "Images/GhostTown";
        sprite = Resources.Load<Sprite>(path);
        Debug.Log(sprite);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (pasos == 1) SceneManager.LoadScene(sceneName: "MenuScene");
            else
            {   
                image.sprite = sprite;
                ++pasos;
            }
            
        }        
    }
}
