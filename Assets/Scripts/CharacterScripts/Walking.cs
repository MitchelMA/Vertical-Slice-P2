using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float _Speed = 5;
    private Vector2 _input = Vector2.zero;

    private void Update()
    {
        HandleInput();
        var moveVec = _Speed * Time.deltaTime * _input;

        transform.Translate(new Vector3(moveVec.x, 0, moveVec.y));
    }


    private void HandleInput()
    {
        var inp = Vector2.zero;
        inp.x = Input.GetAxisRaw("Horizontal");
        inp.y = Input.GetAxisRaw("Vertical");
        _input = inp.normalized;
    }
}

