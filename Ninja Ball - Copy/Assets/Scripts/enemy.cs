using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    float speed = 3f;
    void FixedUpdate()
    {
        if(GetComponent<Rigidbody2D>().angularVelocity < 200){
            GetComponent<Rigidbody2D>().AddTorque(speed);
        }
    }
}
