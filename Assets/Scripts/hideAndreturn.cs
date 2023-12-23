using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hideAndreturn : MonoBehaviour
{
    public GameObject tapToStart;
    void Start()
    {
        StartCoroutine (hide());
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0){
            SceneManager.LoadScene("lvl 0");
        }
    }
    
    IEnumerator hide(){
        yield return new WaitForSeconds(0.4f);
        tapToStart.SetActive(false);
        StartCoroutine (appear());  
    }
    
    IEnumerator appear(){
        yield return new WaitForSeconds(0.4f);
        tapToStart.SetActive(true);
        StartCoroutine (hide());  
    }
}    