using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;
    
    const float G = 0.00667f;
    public static List<Gravity> GravityObjectList;
    
    //orbit
    [SerializeField] private bool planets = false;
    [SerializeField] private int orbitSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (GravityObjectList == null)
        {
            GravityObjectList = new List<Gravity>();
        }
        GravityObjectList.Add( this );
        
        //orbitting
        if (!planets)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in GravityObjectList)
        {
            //call Attract
            
           if (obj != this)
                Attract(obj);
        }
        
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        
        float forceMagnitude = G * ( rb.mass * otherRb.mass/ Mathf.Pow( distance, 2 ));
        Vector3 GravityForce = forceMagnitude * direction.normalized;
        
        otherRb.AddForce(GravityForce);
    }
}
