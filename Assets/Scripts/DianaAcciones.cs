using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaAcciones : MonoBehaviour
{
    // Indica por que golpe va la diana
    public int dianaGolpeada = 0;
    
    // bool para indicar si la rotacion ha sido activada
    public bool rotationActivated = false;

    void OnCollisionEnter(Collision collision)
    {
        // Cuando un objeto "bullet" colisiona con la diana, se destruye la bala, disminuye el total en la interfaz y dependiendo de por que golpe vaya realizara distintas acciones
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            GameManager.DecNumBalas();
            if (dianaGolpeada == 0)
            {
                GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                dianaGolpeada = 1;
            }
            else if (dianaGolpeada == 1)
            {
                rotationActivated = true;
                dianaGolpeada = 2;
            }
            else if (dianaGolpeada == 2)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (rotationActivated)
        {
            transform.Rotate((50 * Time.deltaTime) * 3, (50 * Time.deltaTime) * 3, (50 * Time.deltaTime) * 3);
        }
    }
}