using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Numero de balas total (texto)
    static int numBalas = 0;
    static int numDianasDestruidas = 0;

    static int numBalasTotal = 0;

    // Busca los objetos de texto
    static public GameObject bala_texto;
    static public GameObject diana_texto;
    static public GameObject fuerza_texto;

    // Instantiate de la diana y lista de posiciones de las dianas
    public GameObject dianaInstanciada;
    GameObject[] posicionesDiana;

    // Temporizador
    GameObject temporizadorCanvas;
    TextMeshProUGUI temporizadorTexto;
    static float totalSegundos;

    GameObject shootButton;
    GameObject aimTarget;

    float precision;

    void Start()
    {
        // Objetos texto
        bala_texto = GameObject.Find("bulletCount");
        diana_texto = GameObject.Find("targetCount");
        fuerza_texto = GameObject.Find("strength");
        shootButton = GameObject.Find("ShootButton");
        shootButton = GameObject.Find("AIM_TARGET");

        // Busca los puntos de reaparicion de las dianas
        posicionesDiana = GameObject.FindGameObjectsWithTag("respawnDiana");

        GenerarNuevaDiana();

        IniciarTiempo();
    }

    private void Update()
    {
        Temporizador();
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

    // Potencia de la bala (texto)
    static public void PotenciaBala()
    {
        fuerza_texto.GetComponent<TMP_Text>().SetText("Fuerza: " + DispararBala.potenciaActual.ToString());
    }

    // Genera una diana en uno de los puntos de reaparicion elegidos al azar
    public void GenerarNuevaDiana()
    {
        if (dianaInstanciada != null)
        {
            GameObject dianaAleatoria;

            dianaAleatoria = Instantiate(dianaInstanciada, new Vector3(Random.Range(-10, 10), Random.Range(3, 6), 10), Quaternion.identity);
        }
    }

    void IniciarTiempo()
    {
        // Temporizador
        totalSegundos = 20;

        temporizadorCanvas = GameObject.Find("time");
        temporizadorTexto = temporizadorCanvas.GetComponent<TextMeshProUGUI>();

        temporizadorTexto.text = "Tiempo restante: " + totalSegundos;
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
        totalSegundos += 3;
    }

    void PantallaFinal()
    {
        if (numBalasTotal == 0 || numDianasDestruidas == 0)
        {
            precision = 0;
        } else
        {
            precision = (numDianasDestruidas * 100) / numBalasTotal;
        }
        Debug.Log(precision);
        bala_texto.SetActive(false);
        diana_texto.SetActive(false);
        fuerza_texto.SetActive(false);
        temporizadorCanvas.SetActive(false);
        shootButton.SetActive(false);
        aimTarget.SetActive(false);
        Time.timeScale = 0;
    }
}
