using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour
{
    public GameObject player;
    private Image receta1, receta2, receta3, receta4, receta5;
    private Image timer1, timer2, timer3, timer4, timer5;
    private Sprite ensalada, sopaCebolla ;
    Transform child;
    public  Sprite[] arrayRecetas;
    public float timeRemaining = 30;
    public float timeEndRecipe1 = 50;
    public float timeEndRecipe2 = 50;
    public float timeEndRecipe3 = 50;
    public float timeEndRecipe4 = 50;
    public float timeEndRecipe5 = 50;
    private float maxTimeRecipe = 50;

    // Start is called before the first frame update
    void Start()
    {
        arrayRecetas = new Sprite[7];
       //Init de los sprites de recetas
        ensalada = Resources.Load<Sprite>("Images/Ensalada");
        arrayRecetas[0] = (ensalada);
        sopaCebolla = Resources.Load<Sprite>("Images/RecetaSopaCebolla");
        arrayRecetas[1] = (sopaCebolla);

        //Init Barras de cooldown 
        child = transform.Find("Timer1");
        timer1 = child.GetComponent<Image>();
        timer1.enabled = false;
        child = transform.Find("Timer2");
        timer2 = child.GetComponent<Image>();
        timer2.enabled = false;
        child = transform.Find("Timer3");
        timer3 = child.GetComponent<Image>();
        timer3.enabled = false;
        child = transform.Find("Timer4");
        timer4 = child.GetComponent<Image>();
        timer4.enabled = false;
        child = transform.Find("Timer5");
        timer5 = child.GetComponent<Image>();
        timer5.enabled = false;
        
        //Init de los huecos para mostrar las recetas, y deshabilitarlo hasta que se tengan que usar
        child = transform.Find("Receta1");
        receta1 = child.GetComponent<Image>();
        receta1.enabled = false;
        child = transform.Find("Receta2");
        receta2 = child.GetComponent<Image>();
        receta2.enabled = false;
        child = transform.Find("Receta3");
        receta3 = child.GetComponent<Image>();
        receta3.enabled = false;
        child = transform.Find("Receta4");
        receta4 = child.GetComponent<Image>();
        receta4.enabled = false;
        child = transform.Find("Receta5");
        receta5 = child.GetComponent<Image>();
        receta5.enabled = false;

        if (!receta1.enabled)
        {
            if (!timer1.enabled) timer1.enabled = true;
            int receta = Random.Range(0, 2);
            receta1.enabled = true;
            receta1.sprite = arrayRecetas[receta];
        }



        //Debug.Log("puntuacion???? " + puntuacion + " Get>Text " + gameObject.GetComponent<Text>() );
    }

    // Update is called once per frame
    void Update()
    {
        //Para que cada 30 sec se muestre una receta nueva
        int receta = Random.Range(0, 2);
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            
        }
        else
        {            
            if (!receta1.enabled)
            {
                receta1.enabled = true;
                receta1.sprite = arrayRecetas[receta];
            }
            else if (!receta2.enabled)
            {
                if (!timer2.enabled) timer2.enabled = true;
                receta2.enabled = true;
                receta2.sprite = arrayRecetas[receta];
            }
            else if (!receta3.enabled)
            {
                if (!timer3.enabled) timer3.enabled = true;
                receta3.enabled = true;
                receta3.sprite = arrayRecetas[receta];
            }
            else if (!receta4.enabled)
            {
                if (!timer4.enabled) timer4.enabled = true;
                receta4.enabled = true;
                receta4.sprite = arrayRecetas[receta];
            }
            else if (!receta5.enabled)
            {
                if (!timer5.enabled) timer5.enabled = true;
                receta5.enabled = true;
                receta5.sprite = arrayRecetas[receta];
            }
            timeRemaining = 30;            
        }

        
        updateTimers();

    }

    void updateTimers()
    {
        //Timer 1
        if (timeEndRecipe1 > 0 && receta1.enabled)
        {
            timeEndRecipe1 -= Time.deltaTime;
            if (!timer1.enabled) timer1.enabled = true;
            timer1.fillAmount = timeEndRecipe1 / maxTimeRecipe;
        }
        else if (timeEndRecipe1 <= 0)
        {
            receta1.enabled = false;
            timeEndRecipe1 = 50;
        }

        //Timer 2
        if (timeEndRecipe2 > 0 && receta2.enabled)
        {
            timeEndRecipe2 -= Time.deltaTime;
            if (!timer2.enabled) timer2.enabled = true;
            timer2.fillAmount = timeEndRecipe2 / maxTimeRecipe;
        }
        else if (timeEndRecipe2 <= 0)
        {
            receta2.enabled = false;
            timeEndRecipe2 = 50;
        }
        //Timer 3
        if (timeEndRecipe3 > 0 && receta3.enabled)
        {
            timeEndRecipe3 -= Time.deltaTime;
            if (!timer3.enabled) timer3.enabled = true;
            timer3.fillAmount = timeEndRecipe3 / maxTimeRecipe;
        }
        else if (timeEndRecipe3 <= 0)
        {
            receta3.enabled = false;
            timeEndRecipe3 = 50;
        }
        //Timer 4
        if (timeEndRecipe4 > 0 && receta4.enabled)
        {
            timeEndRecipe4 -= Time.deltaTime;
            if (!timer4.enabled) timer4.enabled = true;
            timer4.fillAmount = timeEndRecipe4 / maxTimeRecipe;
        }
        else if (timeEndRecipe4 <= 0)
        {
            receta4.enabled = false;
            timeEndRecipe4 = 50;
        }
        //Timer 5
        if (timeEndRecipe5 > 0 && receta5.enabled)
        {
            timeEndRecipe5 -= Time.deltaTime;
            if (!timer5.enabled) timer5.enabled = true;
            timer5.fillAmount = timeEndRecipe5 / maxTimeRecipe;
        }
        else if (timeEndRecipe5 <= 0)
        {
            receta5.enabled = false;
            timeEndRecipe5 = 50;
        }
    }
}
