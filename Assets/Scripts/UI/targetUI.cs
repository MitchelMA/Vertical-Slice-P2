using UnityEngine;
using System.Collections;

public class targetUI : MonoBehaviour
{
    public Transform eneOne;
    public Transform eneTwo;
    private Camera MainCamera;
    public GameObject targetIndic;
    public GameObject targetIndic1;
    public Renderer eneRenderOne;
    public Renderer eneRenderTwo;
    public GameObject ene1Shadow;
    public GameObject ene2Shadow;
    private float blinkTime;
    private bool isBlinking;
    public changeUI changeUI;
    

    // Start is called before the first frame update
    void Start(){
        MainCamera = Camera.main;
    }
    void Update(){
        //Continuous rotation on target_indicators
        targetIndic.transform.Rotate(new Vector3(0, 0, -0.5f));
        targetIndic1.transform.Rotate(new Vector3(0, 0, -0.5f));
    }
    public void track(int enemy){
    //Selects a enemy
        if(enemy == 0){
        //First enemy
            ene1Shadow.SetActive(true);
            ene2Shadow.SetActive(false);
            targetIndic.SetActive(true);
            isBlinking = true;

            var screenPos = MainCamera.WorldToScreenPoint(eneOne.position);
            targetIndic.transform.position = screenPos;
            targetIndic1.transform.position = new Vector3(9999, 9999, 9999);
            changeUI.changePlayer1();
            StartCoroutine(delay(enemy));
            StartCoroutine(BlinkChar(enemy));
        }
        else if(enemy == 1){
        //Second enemy
            ene1Shadow.SetActive(false);
            ene2Shadow.SetActive(true);
            targetIndic1.SetActive(true);
            isBlinking = true;
            
            var screenPos = MainCamera.WorldToScreenPoint(eneTwo.position);
            targetIndic1.transform.position = screenPos;
            targetIndic.transform.position = new Vector3(9999, 9999, 9999);
            changeUI.changePlayer2();
            StartCoroutine(delay(enemy));
            StartCoroutine(BlinkChar(enemy));
        }
    }
    IEnumerator delay(int enemy){
    //Hide the indicator_ui_elements after 1.5sec
        yield return new WaitForSeconds(1.5f);
        if(enemy == 0){
            isBlinking = false;
            eneRenderOne.material.SetColor("_Color", new Color(1, 1, 1, 1));
        }   
        else if(enemy == 1){
            isBlinking = false;
            eneRenderTwo.material.SetColor("_Color", new Color(1, 1, 1, 1));
        }
        //Moves target_indicators out of view after 1.5sec
        targetIndic.transform.position = new Vector3(9999, 9999, 9999);
        targetIndic1.transform.position = new Vector3(9999, 9999, 9999);
    }
    IEnumerator BlinkChar(int enemy) {
    //Blinking, Switches enemy sprite between its default color and white
        blinkTime = 0.2f;
        while(isBlinking == true){
            //Checks which enemy is selected and makes it blink 
            if(enemy == 0){
                eneRenderOne.material.SetColor("_Color", new Color(50, 50, 50, 0.5f));
                yield return new WaitForSeconds(blinkTime);
                eneRenderOne.material.SetColor("_Color", new Color(1, 1, 1, 1));
                yield return new WaitForSeconds(blinkTime);
            }
            else if(enemy == 1){
                eneRenderTwo.material.SetColor("_Color", new Color(50, 50, 50, 0.5f));
                yield return new WaitForSeconds(blinkTime);
                eneRenderTwo.material.SetColor("_Color", new Color(1, 1, 1, 1));
                yield return new WaitForSeconds(blinkTime); 
            } 
        }
    }  
}


