using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeUI : MonoBehaviour
{
    public Image player1Long;
    public Image player2Long;
    public Image player3Long;
    public Image player1;
    public Image player2;
    public Image player3;
    
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
    }
    IEnumerator changePlayer1()
    {
        player1Long.transform.Translate(new Vector3(0,-2,0) * speed * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        player2Long.transform.Translate(new Vector3(0,-2,0) * speed * Time.deltaTime);
    }
     IEnumerator changePlayer2()
    {
        player2Long.transform.Translate(new Vector3(0,2,0) * speed * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        player1Long.transform.Translate(new Vector3(0,-2,0) * speed * Time.deltaTime);
    }
     IEnumerator changePlayer3()
    {
        player3Long.transform.Translate(new Vector3(0,2,0) * speed * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        player1Long.transform.Translate(new Vector3(0,-2,0) * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey("1")){
            StartCoroutine(changePlayer1());
        }
        if(Input.GetKey("2")){
            StartCoroutine(changePlayer2());
        }
        if(Input.GetKey("3")){
            StartCoroutine(changePlayer3());
        }
    }
    // void changePlayer1(){
    // }
    // void changePlayer2(){
    //     player1Long.transform.Translate(Vector3.up * speed * Time.deltaTime);
    //     player2Long.transform.Translate(Vector3.down * speed * Time.deltaTime);
    //     player1.transform.Translate(Vector3.up * speed * Time.deltaTime);
    //     player1.transform.Translate(Vector3.down * speed * Time.deltaTime);
    // }
    // void changePlayer3(){
    //     player1Long.transform.Translate(Vector3.up * speed * Time.deltaTime);
    //     player2Long.transform.Translate(Vector3.down * speed * Time.deltaTime);
    //     player1.transform.Translate(Vector3.up * speed * Time.deltaTime);
    //     player1.transform.Translate(Vector3.down * speed * Time.deltaTime);
    // }
}
