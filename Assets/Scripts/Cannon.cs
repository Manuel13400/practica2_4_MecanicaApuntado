using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject aimTarget;
    public GameObject cannon;

    // Start is called before the first frame update
    void Start()
    {
        aimTarget = GameObject.Find("AIM_TARGET");
        cannon = GameObject.Find("CANNON");
    }

    // Update is called once per frame
    void Update()
    {
        cannon.transform.LookAt(aimTarget.transform.position);
    }
}
