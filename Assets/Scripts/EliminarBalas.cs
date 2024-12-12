using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarBalas : MonoBehaviour
{
    // Busca todos los objetos con el tag "bullet", los incluye en una lista y los elimina todos
    public GameObject[] balas;

    private void OnMouseDown()
    {
        balas = GameObject.FindGameObjectsWithTag("bullet");
        foreach (GameObject bala in balas)
        {
            Destroy(bala);
        }

        // Llama al GameManager
        GameManager.ResetearBalas();
    }
}
