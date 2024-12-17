using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Numero de balas total (texto)
    static int numBalas = 0;
    static int numDianasDestruidas = 0;

    // Busca el objeto llamado "Text red" e introduce ahi el texto con la cifra total
    static public GameObject bala_texto;
    static public GameObject diana_texto;
    static public GameObject fuerza_texto;

    public GameObject dianaInstanciada;
    GameObject[] posicionesDiana;

    void Start()
    {
        bala_texto = GameObject.Find("bulletCount");
        diana_texto = GameObject.Find("targetCount");
        fuerza_texto = GameObject.Find("strength");

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

    static public void IncNumDianas()
    {
        numDianasDestruidas++;
        diana_texto.GetComponent<TMP_Text>().SetText("Dianas: " + numDianasDestruidas.ToString());
    }

    static public void PotenciaBala()
    {
        fuerza_texto.GetComponent<TMP_Text>().SetText("Fuerza: " + DispararBala.potenciaActual.ToString());
    }

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
