using UnityEngine;
using System.Collections;

public class targetUI : MonoBehaviour
{
    public Transform eneOne;
    public Transform eneTwo;
    private Camera MainCamera;
    public GameObject targetIndic;
    public Renderer eneRenderOne;
    public Renderer eneRenderTwo;
    public GameObject ene1Shadow;
    public GameObject ene2Shadow;
    public float blinkTime;
    public bool isBlinking;
    private changeUI _changeUI;

    // Start is called before the first frame update
    void Start(){
        MainCamera = Camera.main;
        _changeUI = FindObjectOfType<changeUI>();
    }
    void Update(){
        transform.Rotate(new Vector3(0, 0, -0.5f));
    }
    public void track(int x){
        if(x == 0){
            _changeUI.changePlayer1();
            isBlinking = true;
            _changeUI.changePlayer1();
            ene1Shadow.SetActive(true);
            StartCoroutine(BlinkChar(x));
            targetIndic.SetActive(true);
            var screenPos = MainCamera.WorldToScreenPoint(eneOne.position);
            transform.position = screenPos;
            StartCoroutine(delay(x));
        }
        else if(x == 1){
            _changeUI.changePlayer2();
            isBlinking = true;
            ene2Shadow.SetActive(true);
            StartCoroutine(BlinkChar(x));
            targetIndic.SetActive(true);
            var screenPos = MainCamera.WorldToScreenPoint(eneTwo.position);
            transform.position = screenPos;
            StartCoroutine(delay(x));
        }
    }
    IEnumerator delay(int y){
        yield return new WaitForSeconds(1.5f);
        if(y == 0){
            isBlinking = false;
            eneRenderOne.material.SetColor("_Color", new Color(1, 1, 1, 1));
            ene1Shadow.SetActive(false);
        }   
        else{
            isBlinking = false;
            eneRenderTwo.material.SetColor("_Color", new Color(1, 1, 1, 1));
            ene2Shadow.SetActive(false);
        }
        targetIndic.transform.position = new Vector3(9999, 9999, 9999);
    }
    IEnumerator BlinkChar(int check) {
        blinkTime = 0.2f;
        while(isBlinking == true){
            if(check == 0){
                eneRenderOne.material.SetColor("_Color", new Color(50, 50, 50, 0.5f));
                yield return new WaitForSeconds(blinkTime);
                eneRenderOne.material.SetColor("_Color", new Color(1, 1, 1, 1));
                yield return new WaitForSeconds(blinkTime);
            }
            else if(check == 1){
                eneRenderTwo.material.SetColor("_Color", new Color(50, 50, 50, 0.5f));
                yield return new WaitForSeconds(blinkTime);
                eneRenderTwo.material.SetColor("_Color", new Color(1, 1, 1, 1));
                yield return new WaitForSeconds(blinkTime); 
            } 
        }
    }  
}


