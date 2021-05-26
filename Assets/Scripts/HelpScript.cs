using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{
    Transform childp;
    public Button help;

    // Start is called before the first frame update
    void Start()
    {
        childp = transform.Find("Help");
        Debug.Log("HELPPPPPPPPP");
        Button btn2 = childp.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("BOTON HELP"); 
        SceneManager.LoadScene(sceneName: "HelpScene");
    }
}
