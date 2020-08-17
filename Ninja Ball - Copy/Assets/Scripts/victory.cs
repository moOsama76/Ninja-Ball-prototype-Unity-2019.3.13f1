using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victory : MonoBehaviour
{
    public GameObject victoryLable, ball;
    GameObject up, down, right, left;
    public bool abilityToWin = true;
    GameObject [] edges;

    void Start(){

        edges = GameObject.FindGameObjectsWithTag("vicEdge");

        float maxX = -1, maxY = -1;
        float minX = 9999999, minY = 9999999;

        for(int i = 0; i < edges.Length; i++){

            if(getPos(edges[i], 'x') > maxX){
                maxX = getPos(edges[i], 'x');
                right = edges[i];
            }

            if(getPos(edges[i], 'y') > maxY){
                maxY = getPos(edges[i], 'y');
                up = edges[i];
            }

            if(getPos(edges[i], 'x') < minX){
                minX = getPos(edges[i], 'x');
                left = edges[i];
            }

            if(getPos(edges[i], 'y') < minY){
                minY = getPos(edges[i], 'y');
                down = edges[i];
            }
        }
    }

    void Update()
    {   
        if(getPos(ball, 'x') > getPos(left, 'x') && getPos(ball, 'x') < getPos(right, 'x') &&
        getPos(ball, 'y') < getPos(up, 'y') && getPos(ball, 'y') > getPos(down, 'y') && abilityToWin)
        {
            vic();                         
        }
    }
        
    void vic(){
        victoryLable.SetActive(true);
        ball.GetComponent<BallControl>().isGameActive = false;
        ball.GetComponent<BallControl>().abilityToLose = false;
        if(Time.timeScale != 1){
            Time.timeScale = 1;
        }    
    } 

    float getPos(GameObject GO, char axis){
        if(axis == 'x')
            return GO.transform.position.x;
        
        return GO.transform.position.y;
    }
}


