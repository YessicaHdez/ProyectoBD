using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemExplode : MonoBehaviour
{
    public GameObject pickupEffect;

    public void Pickup()
    {
        GameObject newPickUpEffect = (GameObject) Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(newPickUpEffect, 1);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
