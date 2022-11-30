using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    private float _speed;
    private Vector3 _dir;
    public string enemyTag;

    [SerializeField]
    private Rigidbody rigidBody;
    private bool _wasDropped;

    public bool WasDropped
    {
        get => _wasDropped;
        set
        {
            if (value == _wasDropped)
                return;

            _wasDropped = value;
            rigidBody.useGravity = _wasDropped;
            _speed = 0f;
        }
    }

    shootDodgeball ShootDodgeball;

    // Start is called before the first frame update
    void Start()
    {
        _wasDropped=false;
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            Debug.Log("oui");
            WasDropped = true;
        }
    }
}
