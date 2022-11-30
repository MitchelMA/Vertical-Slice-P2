using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    private float _speed;
    private Vector3 _dir;
    
    [SerializeField]
    private Rigidbody2D rigidBody;
    private bool _wasDropped;

    public bool WasDropped
    {
        get => _wasDropped;
        set
        {
            if (value == _wasDropped)
                return;

            _wasDropped = value;
            rigidBody.gravityScale = Convert.ToSingle(!_wasDropped);
        }
    }

    shootDodgeball ShootDodgeball;

    // Start is called before the first frame update
    void Start()
    {
        ShootDodgeball = FindObjectOfType<shootDodgeball>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _speed * Time.deltaTime * _dir;
    }

    public void Setup(Vector3 dir, Vector3 startPos, float speed = 1f)
    {
        transform.position = startPos;
        _dir = dir.normalized;
        this._speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy1")
        {
            WasDropped = true;
        }
    }
}
