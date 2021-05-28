using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interfaz : MonoBehaviour
{
    public GameObject player;
    private Image receta1, receta2, receta3, receta4, receta5, recetaHelp;
    private Image timer1, timer2, timer3, timer4, timer5, reloj;
    private Sprite ensalada, sopaCebolla,sopaTomate, sopaZanah, ensaladaSola, burguer, burguerqueso ;
    private Sprite helpEnsalada, helpBurguer, helpSopa;
    Transform child;
    public Button nivel1,nivel2,nivel3,nivel4,nivel5;
    public Text textNivel,textQuemado, textTimer;
    
    public  Sprite[] arrayRecetas;
    public float timeRemaining = 30;
    public float timeEndRecipe1 = 50;
    public float timeEndRecipe2 = 50;
    public float timeEndRecipe3 = 50;
    public float timeEndRecipe4 = 50;
    public float timeEndRecipe5 = 50;
    private float maxTimeRecipe = 50;
    private float numReceta;

    public AudioClip recetaSound;

    AudioSource fuenteAudio;

    public int puntuacion = 0;

    //El nivel 1 seran 2 minutos
    public float levelTime =  120.0f;

    public bool GodModeNoQuemar = false;
    public bool GodModeEndCook = false;
    
    private bool firstRecipe = true;

    // Start is called before the first frame update
    void Start()
    {
        if (StaticScenes.numEscena == 0) StaticScenes.numEscena = 1;
        fuenteAudio = GetComponent<AudioSource>();

        //Niveles GodMode
        child = transform.Find("Nivell1");
        nivel1 = child.GetComponent<Button>();
        nivel1.onClick.AddListener(TaskOnClick1);
        nivel1.gameObject.SetActive(false);

        child = transform.Find("Nivell2");
        nivel2 = child.GetComponent<Button>();
        nivel2.onClick.AddListener(TaskOnClick2);
        nivel2.gameObject.SetActive(false);

        child = transform.Find("Nivell3");
        nivel3 = child.GetComponent<Button>();
        nivel3.onClick.AddListener(TaskOnClick3);
        nivel3.gameObject.SetActive(false);

        child = transform.Find("Nivell4");
        nivel4 = child.GetComponent<Button>();
        nivel4.onClick.AddListener(TaskOnClick4);
        nivel4.gameObject.SetActive(false);

        child = transform.Find("Nivell5");
        nivel5 = child.GetComponent<Button>();
        nivel5.onClick.AddListener(TaskOnClick5);
        nivel5.gameObject.SetActive(false);

        child = transform.Find("TextNivel");
        textNivel = child.GetComponent<Text>();
        textNivel.gameObject.SetActive(false);

        //No se puede quemar godmode
        child = transform.Find("TextNoQuemar");
        textQuemado = child.GetComponent<Text>();
        textQuemado.gameObject.SetActive(false);

        //Reloj timer
        child = transform.Find("TimerNum");
        textTimer = child.GetComponent<Text>();
        textTimer.gameObject.SetActive(false);

        child = transform.Find("Reloj");
        reloj = child.GetComponent<Image>();
        reloj.enabled = false; ;


        //Recetas
        arrayRecetas = new Sprite[7];
        //Init de los sprites de recetas
        ensalada = Resources.Load<Sprite>("Images/Ensalada");
        arrayRecetas[0] = (ensalada);
        ensaladaSola = Resources.Load<Sprite>("Images/EnsaladaSimple");
        arrayRecetas[1] = (ensaladaSola);
        sopaTomate = Resources.Load<Sprite>("Images/RecetaSopaTomate");
        arrayRecetas[2] = (sopaTomate);
        sopaZanah = Resources.Load<Sprite>("Images/RecetaSopaZanahoria");
        arrayRecetas[3] = (sopaZanah);
        sopaCebolla = Resources.Load<Sprite>("Images/RecetaSopaCebolla");
        arrayRecetas[4] = (sopaCebolla);
        burguer = Resources.Load<Sprite>("Images/HamburguesaL");
        arrayRecetas[5] = (burguer);
        burguerqueso = Resources.Load<Sprite>("Images/HamburguesaQ");
        arrayRecetas[6] = (burguerqueso);

        child = transform.Find("RecipeHelp");
        recetaHelp = child.GetComponent<Image>();

        if (StaticScenes.numEscena == 1)
        {
            helpEnsalada = Resources.Load<Sprite>("Images/RecetaEnsalada");
            recetaHelp.sprite = helpEnsalada;
        }
        else if (StaticScenes.numEscena == 2)
        {
            helpBurguer = Resources.Load<Sprite>("Images/RecetaBurguer");
            recetaHelp.sprite = helpBurguer;
        }
        else if (StaticScenes.numEscena == 3)
        {
            helpSopa = Resources.Load<Sprite>("Images/RecetaSopa");
            recetaHelp.sprite = helpSopa;
        }
        else if (StaticScenes.numEscena == 4)
        {
            helpBurguer = Resources.Load<Sprite>("Images/RecetaBurguer");
            recetaHelp.sprite = helpBurguer;
        }
        else if (StaticScenes.numEscena == 5)
        {
            helpSopa = Resources.Load<Sprite>("Images/RecetaSopa");
            recetaHelp.sprite = helpSopa;
        }


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

        



        //Debug.Log("puntuacion???? " + puntuacion + " Get>Text " + gameObject.GetComponent<Text>() );
    }

    // Update is called once per frame
    void Update()
    {      

        //Si le das a alguna tecla desaparece la receta principal y se puede jugar
        if (Input.anyKeyDown) { 
            if (recetaHelp.enabled)
            {
                recetaHelp.enabled = false;
                firstRecipe = false;
                
            } 
        }

        if (!firstRecipe)
        {
            if (!reloj.enabled)
            {
                reloj.enabled = true;
                textTimer.gameObject.SetActive(true);
            }

            //GODMODE 1: Carrega el nivell que vulguis -> La 'L'
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Pulsando L");
                
                if (!nivel1.gameObject.activeSelf) {
                    textNivel.gameObject.SetActive(true);
                    nivel1.gameObject.SetActive(true);
                    nivel2.gameObject.SetActive(true);
                    nivel3.gameObject.SetActive(true);
                    nivel4.gameObject.SetActive(true);
                    nivel5.gameObject.SetActive(true);
                }
                else
                {
                    textNivel.gameObject.SetActive(false);
                    nivel1.gameObject.SetActive(false);
                    nivel2.gameObject.SetActive(false);
                    nivel3.gameObject.SetActive(false);
                    nivel4.gameObject.SetActive(false);
                    nivel5.gameObject.SetActive(false);
                }
            }
            //GODMODE 2: Fer que al xef li aparegui la seguent recepta -> La 'P'
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("La P");
                float minTime = timeEndRecipe1;
                int num = 1; 
                if(minTime > timeEndRecipe2)
                {
                    minTime = timeEndRecipe2;
                    num = 2;
                }
                if (minTime > timeEndRecipe3)
                {
                    minTime = timeEndRecipe3;
                    num = 3;
                }
                if (minTime > timeEndRecipe4)
                {
                    minTime = timeEndRecipe4;
                    num = 4;
                }
                if (minTime > timeEndRecipe5)
                {
                    minTime = timeEndRecipe5;
                    num = 5;
                }

                Debug.Log("minTime: "+minTime);
                Debug.Log("num: " + num);

                Debug.Log("generateReceipt: " + receta1.sprite.name);
                if (num == 1)
                {                   
                    player.GetComponent<PickUpObject>().generateReceipt(receta1.sprite.name);
                }
                else if (num == 2)
                {
                    player.GetComponent<PickUpObject>().generateReceipt(receta2.sprite.name);
                }
                else if (num == 3)
                {
                    player.GetComponent<PickUpObject>().generateReceipt(receta3.sprite.name);
                }
                else if (num == 4)
                {
                    player.GetComponent<PickUpObject>().generateReceipt(receta4.sprite.name);
                }
                else if (num == 5)
                {
                    player.GetComponent<PickUpObject>().generateReceipt(receta5.sprite.name);
                }

            }
            //GODMODE 3: Es cuina directament -> La 'K'
            if (Input.GetKeyDown(KeyCode.K))
            {

                if (!GodModeEndCook)
                {
                    GodModeEndCook = true;

                }
                else
                {
                    GodModeEndCook = false;
                }
            }
            //GODMODE 4: Res es pot cremar -> La 'O'
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (!GodModeNoQuemar)
                {
                    GodModeNoQuemar = true;
                    textQuemado.gameObject.SetActive(true);

                }
                else { 
                    GodModeNoQuemar = false;
                    textQuemado.gameObject.SetActive(false);
                }
            }
            

            if (levelTime <= 0 )
            {
                //Salta a la siguiente escena
                Debug.Log("NEXT LEVEL !!!!!!!!");
                
                StaticScenes.puntuacion = puntuacion;
                if (StaticScenes.numEscena == 1)
                {//Si es escena 1
                    StaticScenes.numEscena = 2;
                    //SceneManager.LoadScene(sceneName: "SampleScene2");
                }
                else if (StaticScenes.numEscena == 2)
                {//Si es escena 1
                    StaticScenes.numEscena = 3;
                    //SceneManager.LoadScene(sceneName: "SampleScene3");
                }
                else if (StaticScenes.numEscena == 3)
                {//Si es escena 1
                    StaticScenes.numEscena = 4;
                    //SceneManager.LoadScene(sceneName: "SampleScene4");
                }
                else if (StaticScenes.numEscena == 4)
                {//Si es escena 1
                    StaticScenes.numEscena = 5;
                   // SceneManager.LoadScene(sceneName: "SampleScene5");
                }
                Debug.Log("Que escena es esta??????? " + StaticScenes.numEscena);
                SceneManager.LoadScene(sceneName: "EndLevel");

            }

            int receta = 0;
            if(StaticScenes.numEscena == 1)receta = Random.Range(0, 2);
            else if (StaticScenes.numEscena == 2 || StaticScenes.numEscena == 4) receta = Random.Range(5, 7);
            else if(StaticScenes.numEscena == 3 || StaticScenes.numEscena == 5) receta = Random.Range(2, 5);

            //Para que cada 30 sec se muestre una receta nueva
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                

                if (!receta1.enabled)
                {
                    fuenteAudio.clip = recetaSound;
                    fuenteAudio.Play();
                    if (!timer1.enabled) timer1.enabled = true;

                    receta1.enabled = true;
                    receta1.sprite = arrayRecetas[receta];
                }
            }
            else
            {
                if (!receta1.enabled)
                {
                    fuenteAudio.clip = recetaSound;
                    fuenteAudio.Play();
                    receta1.enabled = true;
                    receta1.sprite = arrayRecetas[receta];
                }
                else if (!receta2.enabled)
                {
                    fuenteAudio.clip = recetaSound;
                    fuenteAudio.Play();
                    if (!timer2.enabled) timer2.enabled = true;
                    receta2.enabled = true;
                    receta2.sprite = arrayRecetas[receta];
                }
                else if (!receta3.enabled)
                {
                    fuenteAudio.clip = recetaSound;
                    fuenteAudio.Play();
                    if (!timer3.enabled) timer3.enabled = true;
                    receta3.enabled = true;
                    receta3.sprite = arrayRecetas[receta];
                }
                else if (!receta4.enabled)
                {
                    fuenteAudio.clip = recetaSound;
                    fuenteAudio.Play();
                    if (!timer4.enabled) timer4.enabled = true;
                    receta4.enabled = true;
                    receta4.sprite = arrayRecetas[receta];
                }
                else if (!receta5.enabled)
                {
                    fuenteAudio.clip = recetaSound;
                    fuenteAudio.Play();
                    if (!timer5.enabled) timer5.enabled = true;
                    receta5.enabled = true;
                    receta5.sprite = arrayRecetas[receta];
                }
                timeRemaining = 30;
            }
            //Disminuir el tiempo de nivel
            levelTime -= Time.deltaTime;
            float min = levelTime / 60.0f;
            //Debug.Log("MIN" + min);
           
            textTimer.text = levelTime.ToString("f0");
            //textTimer.text = min.ToString("0.#0");

            updateTimers();
        }          
    }

    public void updateRecetas(string tipo)
    {
        bool hasActualizado = false;
        Debug.Log("SPRITE? " + receta2.sprite);
        if (receta1.enabled)
        {
            if (tipo == "EnsaladaL" && receta1.sprite.name == "EnsaladaSimple")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 10;
                hasActualizado = true;
            }
            else if (tipo == "EnsaladaLT" && receta1.sprite.name == "Ensalada")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 15;
                hasActualizado = true;
            }
            else if (tipo == "SopaT" && receta1.sprite.name == "RecetaSopaTomate")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaZ" && receta1.sprite.name == "RecetaSopaZanahoria")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaC" && receta1.sprite.name == "RecetaSopaCebolla")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaQC" && receta1.sprite.name == "HamburguesaQ")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaLC" && receta1.sprite.name == "HamburguesaL")
            {
                receta1.enabled = false;
                timer1.enabled = false;
                timeEndRecipe1 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
        }
        if (receta2.enabled && !hasActualizado)
        {
            Debug.Log("receta2? PRE " + receta2.enabled);
            if (tipo == "EnsaladaL" && receta2.sprite.name == "EnsaladaSimple")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 10;
                hasActualizado = true;
            }
            else if (tipo == "EnsaladaLT" && receta2.sprite.name == "Ensalada")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 15;
                hasActualizado = true;
            }
            else if (tipo == "SopaT" && receta2.sprite.name == "RecetaSopaTomate")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaZ" && receta2.sprite.name == "RecetaSopaZanahoria")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaC" && receta2.sprite.name == "RecetaSopaCebolla")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaQC" && receta2.sprite.name == "HamburguesaQ")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaLC" && receta2.sprite.name == "HamburguesaL")
            {
                receta2.enabled = false;
                timer2.enabled = false;
                timeEndRecipe2 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
            Debug.Log("receta2? POST " + receta2.enabled);
        }
        if (receta3.enabled && !hasActualizado)
        {
            if (tipo == "EnsaladaL" && receta3.sprite.name == "EnsaladaSimple")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 10;
                hasActualizado = true;
            }
            else if (tipo == "EnsaladaLT" && receta3.sprite.name == "Ensalada")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 15;
                hasActualizado = true;
            }
            else if (tipo == "SopaT" && receta3.sprite.name == "RecetaSopaTomate")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaZ" && receta3.sprite.name == "RecetaSopaZanahoria")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaC" && receta3.sprite.name == "RecetaSopaCebolla")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaQC" && receta3.sprite.name == "HamburguesaQ")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaLC" && receta3.sprite.name == "HamburguesaL")
            {
                receta3.enabled = false;
                timer3.enabled = false;
                timeEndRecipe3 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
        }
        if (receta4.enabled && !hasActualizado)
        {
            if (tipo == "EnsaladaL" && receta4.sprite.name == "EnsaladaSimple")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 10;
                hasActualizado = true;
            }
            else if (tipo == "EnsaladaLT" && receta4.sprite.name == "Ensalada")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 15;
                hasActualizado = true;
            }
            else if (tipo == "SopaT" && receta4.sprite.name == "RecetaSopaTomate")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaZ" && receta4.sprite.name == "RecetaSopaZanahoria")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaC" && receta4.sprite.name == "RecetaSopaCebolla")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaQC" && receta4.sprite.name == "HamburguesaQ")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaLC" && receta4.sprite.name == "HamburguesaL")
            {
                receta4.enabled = false;
                timer4.enabled = false;
                timeEndRecipe4 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
        }
        if (receta5.enabled && !hasActualizado)
        {
            if (tipo == "EnsaladaL" && receta5.sprite.name == "EnsaladaSimple")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 10;
                hasActualizado = true;
            }
            else if (tipo == "EnsaladaLT" && receta5.sprite.name == "Ensalada")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 15;
                hasActualizado = true;
            }
            else if (tipo == "SopaT" && receta5.sprite.name == "RecetaSopaTomate")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaZ" && receta5.sprite.name == "RecetaSopaZanahoria")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "SopaC" && receta5.sprite.name == "RecetaSopaCebolla")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 25;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaQC" && receta5.sprite.name == "HamburguesaQ")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
            else if (tipo == "HamburguesaLC" && receta5.sprite.name == "HamburguesaL")
            {
                receta5.enabled = false;
                timer5.enabled = false;
                timeEndRecipe5 = 50;
                puntuacion += 30;
                hasActualizado = true;
            }
        }

    }

    void TaskOnClick1()
    {
        StaticScenes.numEscena = 1;
        SceneManager.LoadScene(sceneName: "SampleScene");
        Debug.Log("You have clicked the button!");
    }
    void TaskOnClick2()
    {
        StaticScenes.numEscena = 2;
        SceneManager.LoadScene(sceneName: "SampleScene2");
        Debug.Log("You have clicked the button!");
    }
    void TaskOnClick3()
    {
        StaticScenes.numEscena = 3;
        SceneManager.LoadScene(sceneName: "SampleScene3");
        Debug.Log("You have clicked the button!");
    }
    void TaskOnClick4()
    {
        Debug.Log("ESTIC 4");
        StaticScenes.numEscena = 4;
        SceneManager.LoadScene(sceneName: "SimpleScene4");
        Debug.Log("You have clicked the button!");
    }
    void TaskOnClick5()
    {
        StaticScenes.numEscena = 5;
        SceneManager.LoadScene(sceneName: "SampleScene5");
        Debug.Log("You have clicked the button!");
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
