using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValoresAleatorios : MonoBehaviour
{
    // Valores para el tamaño, la fuerza y el color
    public GameObject bullet;
    public DispararBala velocidadDisparo;

    public float sizeAleatorio;
    public int velocidadAleatoria;
    static public int colorAleatorio;

    // Establece los valores por defecto de la bala
    void Start()
    {
        bullet.gameObject.transform.localScale = new Vector3(1, 1, 1);
        DispararBala.velocidadDisparo = 1500;
        bullet.GetComponent<Renderer>().sharedMaterial.color = Color.yellow;
    }

    // Elige nuevos valores al azar para la bala
    void OnMouseDown()
    {
        sizeAleatorio = Random.Range(0.5f, 4f);
        bullet.gameObject.transform.localScale = new Vector3(sizeAleatorio, sizeAleatorio, sizeAleatorio);

        velocidadAleatoria = Random.Range(500, 2500);
        DispararBala.velocidadDisparo = velocidadAleatoria;

        colorAleatorio = Random.Range(1, 6);
    }
}