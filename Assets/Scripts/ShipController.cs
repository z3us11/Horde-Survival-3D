using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject cursor;
    [Space]
    public float shipThrustForce;
    public float shipBoostForce;
    public float shipTurnForce;

    Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 dir = cursor.transform.position - transform.position;
        //Debug.Log(dir.magnitude);
        if(dir.magnitude > 1f)
        {
            //rb.AddForce(dir * shipThrustForce * Time.deltaTime);
            transform.LookAt(dir);
        }
        
    }
}
