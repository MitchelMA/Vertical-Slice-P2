using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Walking : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Side side;
    public float speed = 5f;
    [NonSerialized] public float CurrentSpeed;
    private Vector2 _input = Vector2.zero;

    private Vector4 _bounds;

    private void Awake()
    {
        CurrentSpeed = speed;
        _bounds = Bounds.Instance[side];
    }

    private void Update()
    {
        HandleInput();
        var moveVec = CurrentSpeed * Time.deltaTime * _input;

        var nextPos = transform.position + new Vector3(moveVec.x, 0, moveVec.y);
        
        if(nextPos.IsInBounds(_bounds))
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

