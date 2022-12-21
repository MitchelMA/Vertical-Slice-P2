using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float _speed = 5;
    private Vector2 _input = Vector2.zero;

    private void Update()
    {
        HandleInput();
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(_input.x, 0, _input.y));
    }


    private void HandleInput()
    {
        var inp = Vector2.zero;
        inp.x = Input.GetAxisRaw("Horizontal");
        inp.y = Input.GetAxisRaw("Vertical");
        _input = inp.normalized;
        _input *= _speed * Time.deltaTime;
    }
}

