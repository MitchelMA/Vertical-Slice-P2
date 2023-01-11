using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Walking : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; 
    public float speed = 5f;
    [NonSerialized]
    public float CurrentSpeed;
    private Vector2 _input = Vector2.zero;

    private void Awake()
    {
        CurrentSpeed = speed;
    }

    private void Update()
    {
        HandleInput();
        var moveVec = CurrentSpeed * Time.deltaTime * _input;

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

