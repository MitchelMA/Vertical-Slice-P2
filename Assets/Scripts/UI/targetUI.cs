using UnityEngine;
using System.Collections;

public class targetUI : MonoBehaviour
{
    [SerializeField] private Vector2 positionalOffset = Vector2.zero;
    [SerializeField] private Character startTarget;
    public float blinkTime;
    public bool isBlinking;
    private changeUI _changeUI;
    private Character _currentTarget;
    private Camera _currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        _currentCamera = GameCam.Instance.GetComponent<Camera>();
        _changeUI = FindObjectOfType<changeUI>();
        _currentTarget = startTarget;
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -0.5f));
        Track();
    }
    private void Track()
    {
        var screenPos = _currentCamera.WorldToScreenPoint(_currentTarget.transform.position);
        screenPos.x += positionalOffset.x;
        screenPos.y += positionalOffset.y;
        transform.position = screenPos;
    }

    public void SetTarget(Character target)
    {
        _currentTarget = target;
    }

    IEnumerator delay(int y)
    {
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector3(9999, 9999, 9999);
        /*  if (y == 0)
          {
              isBlinking = false;
              *//*eneRenderOne.material.SetColor("_Color", new Color(1, 1, 1, 1));
              ene1Shadow.SetActive(false);*//*
          }
          else
          {
              isBlinking = false;
              *//*eneRenderTwo.material.SetColor("_Color", new Color(1, 1, 1, 1));
              ene2Shadow.SetActive(false);*//*
          }*/

    }
  /*  IEnumerator BlinkChar(int check)
    {
        blinkTime = 0.2f;
        while (isBlinking == true)
        {
            if (check == 0)
            {
                eneRenderOne.material.SetColor("_Color", new Color(50, 50, 50, 0.5f));
                yield return new WaitForSeconds(blinkTime);
                eneRenderOne.material.SetColor("_Color", new Color(1, 1, 1, 1));
                yield return new WaitForSeconds(blinkTime);
            }
            else if (check == 1)
            {
                eneRenderTwo.material.SetColor("_Color", new Color(50, 50, 50, 0.5f));
                yield return new WaitForSeconds(blinkTime);
                eneRenderTwo.material.SetColor("_Color", new Color(1, 1, 1, 1));
                yield return new WaitForSeconds(blinkTime);
            }
        }
    }*/
}


