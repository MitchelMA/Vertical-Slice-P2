using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class changeUI : MonoBehaviour
{
    public RectTransform player1Long;
    public RectTransform player2Long;
    public RectTransform player1;
    public RectTransform player2;
    public float speed = 0;
    bool status1 = false;
    bool status2 = false;
 
    public void changePlayer1(){
        if(status1 == false){
            status2 = false;
            LeanTween.move(player2Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player1Long, new Vector3(630,450,0f), speed).setDelay(0.5f);
            LeanTween.move(player1, new Vector3(530,-610,0f), speed);
            LeanTween.move(player2, new Vector3(530,-450,0f), speed).setDelay(0.5f);
        }
        status1 = true;
    }
    public void changePlayer2(){
        if(status2 == false){
            status1 = false;
            LeanTween.move(player1Long, new Vector3(630,610,0f), speed);
            LeanTween.move(player2Long, new Vector3(630,450,0f), speed).setDelay(0.5f);
            LeanTween.move(player2, new Vector3(530,-610,0f), speed);
            LeanTween.move(player1, new Vector3(530,-450,0f), speed).setDelay(0.5f);
        }
        status2 = true;
    }
}
