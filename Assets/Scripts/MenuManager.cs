using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Interfaz
    GameObject dificultadText;
    GameObject jugarButton;
    GameObject creditosButton;
    GameObject creditosText;
    GameObject hideCreditos;
    GameObject easyButton;
    GameObject hardButton;

    // Busca el objeto del texto y establece la dificultad por defecto a facil
    // Busca el resto de elementos del menu para ocultarlos o mostrarlos cuando sea necesario
    void Start()
    {
        dificultadText = GameObject.Find("textoDificultad");
        jugarButton = GameObject.Find("JUGAR");
        creditosButton = GameObject.Find("CREDITOS");
        creditosText = GameObject.Find("textoCreditos");
        hideCreditos = GameObject.Find("VOLVER");
        easyButton = GameObject.Find("FACIL");
        hardButton = GameObject.Find("DIFICIL");

        creditosText.SetActive(false);
        hideCreditos.SetActive(false);

        PlayerPrefs.SetInt("Dificultad", 1);
    }

    // Obtiene el valor de la dificultad y establece el texto segun la elegida
    void Update()
    {
        int valorDificultad = PlayerPrefs.GetInt("Dificultad");
        if (valorDificultad == 1)
        {
            dificultadText.GetComponent<TMP_Text>().SetText("DIFICULTAD: FACIL\nSELECCIONA");
        }
        else
        {
            dificultadText.GetComponent<TMP_Text>().SetText("DIFICULTAD: DIFICIL\nSELECCIONA");
        }
    }

    // Funcion para iniciar la escena del juego
    public void StartGame() { SceneManager.LoadScene(1); }

    // Funciones para establecer la dificultad del juego
    public void Easy() { PlayerPrefs.SetInt("Dificultad", 1); }

    public void Hard() { PlayerPrefs.SetInt("Dificultad", 2); }

    // Funcion para mostrar creditos
    public void ShowCredits()
    {
        creditosText.SetActive(true);
        hideCreditos.SetActive(true);

        dificultadText.SetActive(false);
        jugarButton.SetActive(false);
        creditosButton.SetActive(false);
        jugarButton.SetActive(false);
        easyButton.SetActive(false);
        hardButton.SetActive(false);
    }

    // Funcion para ocultar creditos
    public void HideCredits()
    {
        creditosText.SetActive(false);
        hideCreditos.SetActive(false);

        dificultadText.SetActive(true);
        jugarButton.SetActive(true);
        creditosButton.SetActive(true);
        jugarButton.SetActive(true);
        easyButton.SetActive(true);
        hardButton.SetActive(true);
    }
}
