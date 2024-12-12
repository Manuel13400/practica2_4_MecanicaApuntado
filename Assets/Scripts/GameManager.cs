using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Numero de balas total (texto)
    static int numBalas = 0;

    // Busca el objeto llamado "Text red" e introduce ahi el texto con la cifra total
    static public GameObject bala_texto;
    void Start()
    {
        bala_texto = GameObject.Find("Text red");
    }

    // Reinicia el total de balas (texto)
    static public void ResetearBalas()
    {
        numBalas = 0;
        bala_texto.GetComponent<TMP_Text>().SetText(numBalas.ToString());
    }

    // Incrementa el numero de balas (texto)
    static public void IncNumBalas()
    {
        numBalas++;
        bala_texto.GetComponent<TMP_Text>().SetText(numBalas.ToString());
    }

    // Reduce el numero de balas (texto)
    static public void DecNumBalas()
    {
        if (numBalas > 0)
        {
            numBalas--;
            bala_texto.GetComponent<TMP_Text>().SetText(numBalas.ToString());
        }
    }
}
