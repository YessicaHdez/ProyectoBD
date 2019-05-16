using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    int counter;
    float current_angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(current_angle == 360)
        {
            current_angle = 0;
        }
        else
        {
            current_angle++;
        }

              Quaternion target = Quaternion.Euler(0, current_angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5);

    }
}
