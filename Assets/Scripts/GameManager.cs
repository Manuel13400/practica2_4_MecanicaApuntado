using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Numero de balas total (texto)
    static int numBalas = 0;
    static int numDianasDestruidas = 0;

    // Busca los objetos de texto
    static public GameObject bala_texto;
    static public GameObject diana_texto;
    static public GameObject fuerza_texto;

    // Instantiate de la diana y lista de posiciones de las dianas
    public GameObject dianaInstanciada;
    GameObject[] posicionesDiana;

    void Start()
    {
        // Objetos texto
        bala_texto = GameObject.Find("bulletCount");
        diana_texto = GameObject.Find("targetCount");
        fuerza_texto = GameObject.Find("strength");

        // Busca los puntos de reaparicion de las dianas
        posicionesDiana = GameObject.FindGameObjectsWithTag("respawnDiana");

        GenerarNuevaDiana();
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
        if (dianaInstanciada != null && posicionesDiana.Length > 0)
        {
            int numeroAleatorio = Random.Range(0, posicionesDiana.Length);
            GameObject dianaAleatoria = posicionesDiana[numeroAleatorio];

            dianaAleatoria = Instantiate(dianaInstanciada, dianaAleatoria.transform.position, Quaternion.identity);
        }
    }
}
