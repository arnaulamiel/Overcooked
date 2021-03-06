using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour
{
    public Button help, godmode;
    Transform childh;

    // Start is called before the first frame update
    void Start()
    {
        childh = transform.Find("Button");
        Button btn2 = childh.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClickHelp);

        childh = transform.Find("godmode");
        godmode = childh.GetComponent<Button>();
        godmode.onClick.AddListener(TaskOnClickGOD);

    }
    void TaskOnClickHelp()
    {
        SceneManager.LoadScene(sceneName: "MenuScene");
    }
    void TaskOnClickGOD()
    {
        SceneManager.LoadScene(sceneName: "GodMode");
    }
}
