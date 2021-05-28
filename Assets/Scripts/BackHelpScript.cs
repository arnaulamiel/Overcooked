using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackHelpScript : MonoBehaviour
{
    public Button help;
    Transform childh;
    // Start is called before the first frame update
    void Start()
    {
        childh = transform.Find("Button");
        Button btn2 = childh.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClickHelp);
    }

    void TaskOnClickHelp()
    {
        SceneManager.LoadScene(sceneName: "HelpScene");
    }
}
