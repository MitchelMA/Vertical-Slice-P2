using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    private float _speed;
    public int damageAmount;
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

    private shootDodgeball ShootDodgeball;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        _wasDropped=false;
        ShootDodgeball = FindObjectOfType<shootDodgeball>();


        if (TryGetComponent<SphereCollider>(out var collider))
        {
            collider.enabled = false;
            float dur = (Vector3.right * 1f).magnitude / _speed;
            yield return new WaitForSeconds(dur);
            collider.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _dir);
    }

    public void Setup(Vector3 dir, Vector3 startPos, float speed = 1f)
    {
        transform.position = startPos;
        _dir = dir.normalized;
        print(_dir);
        _speed = speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        var hitObj = collision.gameObject;
        var CharData = hitObj.GetComponent<Character>();
        if (collision.gameObject.tag == enemyTag)
        {
            CharData.TakeDamage(damageAmount);
            WasDropped = true;
        }
    }


}