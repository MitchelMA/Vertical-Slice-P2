using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeUI : MonoBehaviour
{
    public RectTransform player1Long;
    public RectTransform player2Long;
    public RectTransform player3Long;
    public RectTransform player1;
    public RectTransform player2;
    public RectTransform player3;
    public float speed = 0;
    bool status1 = false;
    bool status2 = false;
    bool status3 = false;
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("1")){
            changePlayer1();
        }
        if(Input.GetKeyDown("2")){
            changePlayer2();
        }
        if(Input.GetKeyDown("3")){
            changePlayer3();
        }
    }
      void changePlayer1(){
        if(status1 == false){
            status2 = false;
            status3 = false;
            LeanTween.move(player2Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player3Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player1Long, new Vector3(630,450,0f), speed).setDelay(0.5f);
            LeanTween.move(player1, new Vector3(530,-610,0f), speed);
            LeanTween.move(player3, new Vector3(760,-450,0f), speed).setDelay(0.5f);
            LeanTween.move(player2, new Vector3(530,-450,0f), speed).setDelay(0.5f);
        }
       status1 = true;
    }
    void changePlayer2(){
        if(status2 == false){
            status3 = false;
            status1 = false;
            LeanTween.move(player3Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player1Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player2Long, new Vector3(630,450,0f), speed).setDelay(0.5f);
            LeanTween.move(player3, new Vector3(760,-450,0f), speed).setDelay(0.5f);
            LeanTween.move(player2, new Vector3(530,-610,0f), speed);
            LeanTween.move(player1, new Vector3(530,-450,0f), speed).setDelay(0.5f);
        }
        status2 = true;
    }
    void changePlayer3(){
        if(status3 == false){
            status1 = false;
            status2 = false;
            LeanTween.move(player1Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player2Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player3Long, new Vector3(630,450,0f), speed).setDelay(0.5f);
            LeanTween.move(player1, new Vector3(530,-610,0f), speed);   
            LeanTween.move(player1, new Vector3(760,-610,0f), speed);
            LeanTween.move(player2, new Vector3(530,-450,0f), speed).setDelay(0.5f);
            LeanTween.move(player3, new Vector3(760,-610,0f), speed);
            LeanTween.move(player1, new Vector3(760,-450,0f), speed).setDelay(0.5f);
        }
       status3 = true;
    }
}
