using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassToLevel : MonoBehaviour
{
    Transform childp;
    Transform childh;

    // Start is called before the first frame update
    void Start()
    {
        childp = transform.Find("Play");
        Button btn = childp.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickPlay);

        /*childp = transform.Find("Help");
        Button btn2 = childh.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClickHelp);*/
    }

    
    void TaskOnClickPlay()
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
        Debug.Log("You have clicked the button!");
    }

   /* void TaskOnClickHelp()
    {

        SceneManager.LoadScene(sceneName: "HelpScene");
        Debug.Log("You have clicked the button!");
    }*/

}
