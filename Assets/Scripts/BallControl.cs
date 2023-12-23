using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{
    Vector2 mousePos, touchPos;
    Vector2 dir;

    float difPositionX, difPositionY;
    float arrowPosX, arrowPosY;
    float angle;
    float diffrenceY;
    float speed = 150;

    public bool isGameActive = true;
    public bool abilityToLose = true;
    bool isSceneLoaded = false;

    public GameObject prevoiusMousePos;
    public GameObject arrow;
    public GameObject bg;
    public GameObject cam;
    public GameObject defeat;
    public GameObject winLine;

    public Touch touch;

    Rigidbody2D ballRB;





    // Start is called before the first frame update
    void Start()
    {
        diffrenceY = cam.transform.position.y - transform.position.y;
        ballRB =  getRB(gameObject);
        StartCoroutine(sceneLoaded());
    }

    // Update is called once per frame
    void Update()
    {

        getRB(cam).velocity = new Vector2(ballRB.velocity.x * (bg.transform.localScale.x / 100), ballRB.velocity.y * (bg.transform.localScale.y / 100));
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Mouse Controls_________________________________________________
        if(isGameActive){
            if(SystemInfo.deviceType == DeviceType.Desktop){
                if(Input.GetMouseButtonDown(0)){

                    prevoiusMousePos.transform.position = mousePos;

                    if(slowMotion())
                        Time.timeScale = 0.1f;

                } else if (Input.GetMouseButton(0)){
                    hold();

                } else if(Input.GetMouseButtonUp(0)){
                    let();

                }
            }
            // Touch Controls_________________________________________________
            else if(SystemInfo.deviceType == DeviceType.Handheld){

                
                
                if(Input.touchCount > 0){
                    touch = Input.GetTouch(0);
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    if(touch.phase  == TouchPhase.Began && isSceneLoaded){

                        prevoiusMousePos.transform.position = touchPos;

                        if(slowMotion())
                            Time.timeScale = 0.1f;
                        

                    } else if(touch.phase  == TouchPhase.Moved && isSceneLoaded){
                        hold();
                    } else if(touch.phase  == TouchPhase.Ended && isSceneLoaded){
                        let();
                    } 
                }
            }
        }

    }

    // Functions' section___________________________________________________________________________________________________________________
    Rigidbody2D getRB(GameObject gamOb){
        return gamOb.GetComponent<Rigidbody2D>();
    }

    float getPos(GameObject GO, char axis){
        if(axis == 'x')
            return GO.transform.position.x;
        
        return GO.transform.position.y;
    }

    Vector2 getPos(GameObject GO){
        return GO.transform.position;
    }

    float sumOfForces(float x, float y){
            return Mathf.Sqrt(x * x + y * y);
    }

    bool slowMotion(){
        if((ballRB.velocity.x) * (ballRB.velocity.x) + (ballRB.velocity.y) * (ballRB.velocity.y) != 0)
            return true;
        
        return false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
         if(col.gameObject.tag == "danger" && abilityToLose){
             Defeat();
         }
    } 

    void Defeat(){
        defeat.SetActive(true);
        winLine.GetComponent<victory>().abilityToWin = false;
        isGameActive = false;
        if(Time.timeScale != 1){
            Time.timeScale = 1;
        }
    }

    void hold(){
        arrow.SetActive(true);
        dir = (getPos(arrow) - getPos(gameObject));
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        difPositionX = getPos(prevoiusMousePos, 'x') - mousePos.x;
        difPositionY = getPos(prevoiusMousePos, 'y') - mousePos.y;
        arrowPosX = (getPos(gameObject, 'x') + difPositionX);
        arrowPosY = (getPos(gameObject, 'y') + difPositionY);
        arrow.transform.position = new Vector2(arrowPosX, arrowPosY);
    }

    void let(){
        arrow.SetActive(false);
        getRB(gameObject).velocity = new Vector2(0,0);
        float forceToAddX = (getPos(prevoiusMousePos, 'x') - mousePos.x) * speed;
        float forceToAddY = (getPos(prevoiusMousePos, 'y') - mousePos.y) * speed;

        // limit over force
        while(sumOfForces(forceToAddX, forceToAddY) > 1200){
            speed--;
            forceToAddX = (getPos(prevoiusMousePos, 'x') - mousePos.x) * speed;
            forceToAddY = (getPos(prevoiusMousePos, 'y') - mousePos.y) * speed;

        }

        getRB(gameObject).AddForce(new Vector2(forceToAddX, forceToAddY));
        speed = 150;
        Time.timeScale = 1f;
        Destroy(GameObject.Find("toturialHand"));
        
    }

    IEnumerator sceneLoaded(){
        yield return new WaitForSeconds(1);
        isSceneLoaded = true;
    }
    
}
