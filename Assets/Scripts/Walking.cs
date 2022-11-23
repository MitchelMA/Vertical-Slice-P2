using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    private Vector3 input;


    private void Update()
    {
        getInput();
    }

    private void FixedUpdate()
    {
        move();
    }

    void getInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void move( )
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }


}
