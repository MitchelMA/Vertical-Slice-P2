using UnityEngine;
using System.Collections;

public class targetUI : MonoBehaviour
{
    public Transform eneOne;
    public Transform eneTwo;
    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }
    public void track(int x){
        if(x == 0){
            var screenPos = MainCamera.WorldToScreenPoint(eneOne.position);
            transform.position = screenPos;
        }
        else if(x == 1){
            var screenPos = MainCamera.WorldToScreenPoint(eneTwo.position);
            transform.position = screenPos;
        }
    }
}