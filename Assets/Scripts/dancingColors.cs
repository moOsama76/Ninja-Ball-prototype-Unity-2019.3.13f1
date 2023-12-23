using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class dancingColors : MonoBehaviour
{
     SpriteRenderer clr;
     public float[] RGB;
     void Start()
    {
        StartCoroutine(changeColor());
    }
    
    IEnumerator changeColor(){
        yield return new WaitForSeconds(0.25f);
        RGB[0] = Random.Range(0, 255);
        RGB[1] = Random.Range(0, 255);
        RGB[2] = Random.Range(0, 255);
        clr = gameObject.GetComponent<SpriteRenderer>();
        clr.GetComponent<SpriteRenderer>().color = new Color(RGB[0]/255, RGB[1]/255, RGB[2]/255, 1f);
        StartCoroutine(changeColor());
    }


}
