using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    public GameObject[] items;
    int randomInt;

    public void Drop ()
    {
        randomInt = Random.Range(0, items.Length);
        Vector3 position = transform.position;
        position.y += 1;
        Instantiate(items[randomInt], position, Quaternion.identity);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
