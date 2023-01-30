using System;
using System.Collections;
using System.Collections.Generic;
using Utils;
using System.Linq;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    private float _speed;
    public int damageAmount;
    private Vector3 _dir;
    public string[] effectTags = new string[2];

    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private AudioSource _audioSource;
    private bool _wasDropped;
    private float _droppedDuration = 0f;

    private bool _hasHitAny = false;

    public float DroppedDuration => _droppedDuration;

    public bool WasDropped
    {
        get => _wasDropped;
        set
        {
            if (value == _wasDropped)
                return;

            if (_wasDropped)
                _droppedDuration = 0;

            _wasDropped = value;
            rigidBody.useGravity = _wasDropped;
            _speed = 0f;
            GetComponent<Transform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }

    // The speed of the dodgeball
    public float Speed => _speed;

    // The normalized direction vector
    public Vector3 Direction => _dir;

    // The movement of the dodgeball
    public Vector3 Movement => _dir * _speed;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        _wasDropped = false;

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
        if (!transform.Translate(Movement * Time.deltaTime, Bounds.Instance.OuterBounds))
            WasDropped = true;
    }

    private void FixedUpdate()
    {
        if (WasDropped)
            _droppedDuration += Time.fixedDeltaTime;
    }

    public void Setup(Vector3 dir, Vector3 startPos, float speed = 1f)
    {
        transform.position = startPos;
        _dir = dir.normalized;
        _speed = speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();

        HandleAny(collision.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();

        HandleAny(collision.gameObject);
    }

    private void HandleAny(GameObject hitObj)
    {
        if (_hasHitAny)
            return;

        _hasHitAny = true;
        var charData = hitObj.GetComponent<Character>();
        if (effectTags.Contains(hitObj.tag) && !WasDropped)
        {
            charData.TakeDamage(damageAmount);
            WasDropped = true;
        }
    }
}