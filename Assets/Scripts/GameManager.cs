using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Variables estaticas
    static int numBalas = 0;
    static int numDianasDestruidas = 0;
    static int numBalasTotal = 0;

    // Objetos de texto
    static public GameObject bala_texto;
    static public GameObject diana_texto;
    //static public GameObject fuerza_texto;

    // Interfaz
    GameObject shootButton;
    GameObject aimTarget;
    GameObject winText;
    GameObject looseText;
    GameObject balasFinalText;
    GameObject dianasFinalText;
    GameObject precisionText;

    // Temporizador
    GameObject temporizadorCanvas;
    TextMeshProUGUI temporizadorTexto;
    static float totalSegundos;

    // Instantiate de la diana y lista de posiciones de las dianas
    public GameObject dianaInstanciada;
    private GameObject dianaActual;
    GameObject[] posicionesDiana;

    // Calculo precision final
    float precision;

    // Dificultad
    static public int valorDificultad;

    // Sonidos
    public AudioClip sonidoVictoria;
    public AudioClip sonidoDerrota;

    private AudioSource audioSource;

    void Start()
    {
        // Buscar objetos al inicio
        bala_texto = GameObject.Find("bulletCount");
        diana_texto = GameObject.Find("targetCount");
        shootButton = GameObject.Find("ShootButton");
        aimTarget = GameObject.Find("AIM_TARGET");

        // Busqueda de los textos para fin de partida para ocultarlos al principio
        winText = GameObject.Find("WinText");
        looseText = GameObject.Find("LooseText");
        balasFinalText = GameObject.Find("BalasFinal");
        dianasFinalText = GameObject.Find("DianasFinal");
        precisionText = GameObject.Find("PrecisionFinal");

        winText.SetActive(false);
        looseText.SetActive(false);
        balasFinalText.SetActive(false);
        dianasFinalText.SetActive(false);
        precisionText.SetActive(false);

        // Busca los puntos de reaparicion de las dianas
        posicionesDiana = GameObject.FindGameObjectsWithTag("respawnDiana");

        // Dificultad
        valorDificultad = PlayerPrefs.GetInt("Dificultad");

        // Sonido
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { audioSource = gameObject.AddComponent<AudioSource>(); }

        // Generar diana inicial y temporizador
        GenerarNuevaDiana();
        IniciarTiempo();
    }

    void Update()
    {
        Temporizador();
    }

    void IniciarTiempo()
    {
        // Temporizador segun dificultad
        if (valorDificultad == 1) { totalSegundos = 20; }
        else { totalSegundos = 15; }

        temporizadorCanvas = GameObject.Find("time");
        temporizadorTexto = temporizadorCanvas.GetComponent<TextMeshProUGUI>();

        temporizadorTexto.text = "Tiempo restante: " + totalSegundos;
    }

    // Genera una diana en uno de los puntos de reaparicion elegidos al azar
    public void GenerarNuevaDiana()
    {
        if (dianaInstanciada != null)
        {
            // Asegurarse de que la diana se destruye antes de crear una nueva
            if (dianaActual != null)
            {
                Destroy(dianaActual);
            }

            GameObject dianaAleatoria;
            float posX, posY;

            // Para la primera diana, se genera en un sitio aleatorio
            if (dianaActual == null)
            {
                posX = Random.Range(-10f, 10f);
                posY = Random.Range(3f, 6f);
            }
            // A partir de la segunda, se tiene en cuenta la dificultad y la posicion de la anterior
            // para generar una nueva diana dentro de un rango cercano a la anterior
            else
            {
                if (valorDificultad == 1)
                {
                    posX = Mathf.Clamp(dianaActual.transform.position.x + Random.Range(-2f, 2f), -10f, 10f);
                    posY = Mathf.Clamp(dianaActual.transform.position.y + Random.Range(-2f, 2f), 3f, 6f);
                }
                else
                {
                    posX = Mathf.Clamp(dianaActual.transform.position.x + Random.Range(-6f, 6f), -10f, 10f);
                    posY = Mathf.Clamp(dianaActual.transform.position.y + Random.Range(-3f, 5f), 3f, 6f);
                }
            }

            // Instancia una nueva diana en las posiciones aleatorias
            dianaAleatoria = Instantiate(dianaInstanciada, new Vector3(posX, posY, 10), Quaternion.identity);

            dianaActual = dianaAleatoria;
        }
    }

    // Reinicia el total de balas (texto)
    static public void ResetearBalas()
    {
        numBalas = 0;
        bala_texto.GetComponent<TMP_Text>().SetText("Balas: " + numBalas.ToString());
    }

    // Incrementa el numero de balas (texto)
    static public void IncNumBalas()
    {
        numBalas++;
        numBalasTotal++;
        bala_texto.GetComponent<TMP_Text>().SetText("Balas: " + numBalas.ToString());
    }

    // Reduce el numero de balas (texto)
    static public void DecNumBalas()
    {
        if (numBalas > 0)
        {
            numBalas--;
            bala_texto.GetComponent<TMP_Text>().SetText("Balas: " + numBalas.ToString());
        }
    }

    // Aumenta el numero de dianas destruidas (texto)
    static public void IncNumDianas()
    {
        numDianasDestruidas++;
        diana_texto.GetComponent<TMP_Text>().SetText("Dianas: " + numDianasDestruidas.ToString());
    }

    void Temporizador()
    {
        // Temporizador
        totalSegundos = totalSegundos - Time.deltaTime;

        if (totalSegundos < 0)
        {
            totalSegundos = 0;
            PantallaFinal();
        }

        float minutos = Mathf.FloorToInt(totalSegundos / 60);
        float segundos = Mathf.FloorToInt(totalSegundos % 60);

        temporizadorTexto.text = "Tiempo restante: " + string.Format("{0:00}:{1:00}", minutos, segundos);

    }

    static public void AddTiempo()
    {
        if (valorDificultad == 1) { totalSegundos += 3; }
        else { totalSegundos += 1; }
    }

    void PantallaFinal()
    {
        // Calculo de precision
        if (numBalasTotal == 0 || numDianasDestruidas == 0) { precision = 0; }
        else { precision = (numDianasDestruidas * 100) / numBalasTotal; }

        // Ocultar elementos de la interfaz
        GameObject[] interfazOcultar = { bala_texto, diana_texto, temporizadorCanvas, shootButton, aimTarget };
        foreach (var elementoInterfaz in interfazOcultar) { elementoInterfaz.SetActive(false); };

        // Dependiendo de la precision mostrar victoria o derrota
        if (precision > 50 && numDianasDestruidas > 10) { 
            winText.SetActive(true);
            ReproducirSonido(sonidoVictoria);
        } 
        else { 
            looseText.SetActive(true);
            ReproducirSonido(sonidoDerrota);
        }

        // Mostrar estadisticas finales
        balasFinalText.SetActive(true);
        dianasFinalText.SetActive(true);
        precisionText.SetActive(true);

        balasFinalText.GetComponent<TMP_Text>().SetText("Balas totales: " + numBalasTotal.ToString());
        dianasFinalText.GetComponent<TMP_Text>().SetText("Dianas totales: " + numDianasDestruidas.ToString());
        precisionText.GetComponent<TMP_Text>().SetText("Precision: " + precision.ToString() + "%");

        Time.timeScale = 0;
    }

    void ReproducirSonido(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Potencia de la bala (texto)
    /*static public void PotenciaBala()
    {
        fuerza_texto.GetComponent<TMP_Text>().SetText("Fuerza: " + DispararBala.potenciaActual.ToString());
    }*/
}
