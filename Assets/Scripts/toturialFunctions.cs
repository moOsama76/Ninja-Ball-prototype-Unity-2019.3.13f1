using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toturialFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-0.02f,0,0);
        if(transform.position.x < -1){
            transform.position = new Vector2(0.5f, -3.2f);
        } 
    }
}
