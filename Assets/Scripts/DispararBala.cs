using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararBala : MonoBehaviour
{
    // Valores para posicion en la que aparece la bala, hacia donde tiene que ir y el cañon
    public GameObject posicionInicial;
    public GameObject posicionFinal;
    public GameObject cannon;

    GameObject aimTarget;

    // Potencia del disparo
    static public float potenciaMinima = 250f;
    static public float potenciaMaxima = 3250f;
    static public float potenciaActual = 0;

    float tiempoInicio;
    float tiempoFinal;

    // Prefab de la bala
    public GameObject bullet;
    GameObject balaInstanciada;

    private void Start()
    {
        aimTarget = GameObject.Find("AIM_TARGET");
    }

    private void Update()
    {
        if (Input.GetKey("w"))  {   aimTarget.transform.position += Vector3.up * 9 * Time.deltaTime;      }
        if (Input.GetKey("s"))  {   aimTarget.transform.position += Vector3.down * 9 * Time.deltaTime;    }

        if (aimTarget.transform.position.y > 6) { aimTarget.transform.position = new Vector3(aimTarget.transform.position.x, 6, aimTarget.transform.position.z); }
        if (aimTarget.transform.position.y < 1) { aimTarget.transform.position = new Vector3(aimTarget.transform.position.x, 1, aimTarget.transform.position.z); }


        if (Input.GetKey("d"))  {   aimTarget.transform.position += Vector3.right * 9 * Time.deltaTime;   }
        if (Input.GetKey("a"))  {   aimTarget.transform.position += Vector3.left * 9 * Time.deltaTime;    }

        if (aimTarget.transform.position.x > 5) { aimTarget.transform.position = new Vector3(5, aimTarget.transform.position.y, aimTarget.transform.position.z); }
        if (aimTarget.transform.position.x < -5) { aimTarget.transform.position = new Vector3(-5, aimTarget.transform.position.y, aimTarget.transform.position.z); }

    }

    private void OnMouseDown()
    {
        tiempoInicio = Time.time;
        cannon.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseUp()
    {
        tiempoFinal = Time.time;

        cannon.GetComponent<Renderer>().material.color = Color.white;

        potenciaActual = ((tiempoFinal - tiempoInicio) * 5) * potenciaMinima;

        if (potenciaActual > potenciaMaxima)
        {
            potenciaActual = potenciaMaxima;
        }

        balaInstanciada = Instantiate(bullet, posicionInicial.transform.position, Quaternion.identity);

        Rigidbody rb = balaInstanciada.GetComponent<Rigidbody>();

        Vector3 direccion = posicionFinal.transform.position - posicionInicial.transform.position;
        rb.AddForce(direccion.normalized * potenciaActual);

        GameManager.PotenciaBala();
        GameManager.IncNumBalas();
    }
}
