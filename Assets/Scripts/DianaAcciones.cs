using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaAcciones : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Cuando un objeto "bullet" colisiona con la diana, se destruye la bala, disminuye el total en la interfaz
        // y ademas, aumenta el numero de dianas destruidas y genera otra en una posicion aleatoria de la lista
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            GameManager.DecNumBalas();
            GameManager.IncNumDianas();
            Destroy(gameObject);

            FindObjectOfType<GameManager>().GenerarNuevaDiana();
        }
    }
}