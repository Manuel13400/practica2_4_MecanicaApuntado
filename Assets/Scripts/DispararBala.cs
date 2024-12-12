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
    static public float velocidadDisparo = 1750f;

    // Prefab de la bala
    public GameObject bullet;
    GameObject balaInstanciada;

    private void Start()
    {
        aimTarget = GameObject.Find("AIM_TARGET");
    }

    private void Update()
    {
        if (Input.GetKey("w"))
        {
            aimTarget.transform.position += Vector3.up * 9 * Time.deltaTime;
        } else if (Input.GetKey("s"))
        {
            aimTarget.transform.position += Vector3.down * 9 * Time.deltaTime;
        } else if (Input.GetKey("d"))
        {
            aimTarget.transform.position += Vector3.right * 9 * Time.deltaTime;
        } else if (Input.GetKey("a"))
        {
            aimTarget.transform.position += Vector3.left * 9 * Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        balaInstanciada = Instantiate(bullet, posicionInicial.transform.position, Quaternion.identity);
        
        Rigidbody rb = balaInstanciada.GetComponent<Rigidbody>();

        Vector3 direccion = posicionFinal.transform.position - posicionInicial.transform.position;
        rb.AddForce(direccion.normalized * velocidadDisparo);
    }
}
