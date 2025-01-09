using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class DianaAcciones : MonoBehaviour
{
    // Tiempo
    public float segundosRestantes;

    // Dificultad
    static public int valorDificultad;

    public AudioClip dianaSonido;
    private AudioSource audioSource;

    void Start()
    {
        // Dificultad
        valorDificultad = PlayerPrefs.GetInt("Dificultad");

        if (valorDificultad == 1) { segundosRestantes = 5; }
        else { segundosRestantes = 3; }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { audioSource = gameObject.AddComponent<AudioSource>(); }
    }

    void Update()
    {
        // Comienza cuenta atras
        segundosRestantes = segundosRestantes - Time.deltaTime;

        // Si la cuenta atras llega a 0 genera una nueva diana y destruye la previa
        if (segundosRestantes < 0)
        {
            FindObjectOfType<GameManager>().GenerarNuevaDiana();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cuando un objeto "bullet" colisiona con la diana, se destruye la bala, disminuye el total en la interfaz
        // y ademas, aumenta el numero de dianas destruidas y añade tiempo al temporizador del cañon
        if (collision.gameObject.CompareTag("bullet"))
        {
            if (dianaSonido != null)
            {
                AudioSource.PlayClipAtPoint(dianaSonido, transform.position);
            }

            Destroy(collision.gameObject);
            GameManager.DecNumBalas();
            GameManager.IncNumDianas();
            GameManager.AddTiempo();

            // Establece los segundos restantes a cero para que la diana se destruya
            segundosRestantes = 0;
        }
    }
}