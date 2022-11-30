using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;

    [SerializeField] private Vector2 standFacingDir = Vector2.right;
    private Vector2 _facingDir;
    private Vector3 _currentPos;

    // public getters
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            if (value == _currentHealth)
                return;
            var prev = _currentHealth;
            _currentHealth = value;

            healthChange.Invoke(new HealthChangeData
            {
                MaxHealth = maxHealth,
                Previous = prev,
                Current = _currentHealth,
            });
        }
    }

    public Vector2 FacingDirection
    {
        get => _facingDir;
        private set
        {
            var newVal = value.normalized;
            const float tol = 0.01f;
            if (Mathf.Abs(_facingDir.x - newVal.x) < tol && Mathf.Abs(_facingDir.y - newVal.y) < tol)
                return;

            var oldVal = _facingDir;
            _facingDir = newVal;
            facingDirChange.Invoke(new FacingDirChange
            {
                Previous = oldVal,
                Current = _facingDir
            }); 
        }
    }

    // Events
    public UnityEvent<HealthChangeData> healthChange = new UnityEvent<HealthChangeData>();
    public UnityEvent<FacingDirChange> facingDirChange = new UnityEvent<FacingDirChange>();

    // Awake is called before Start
    private void Awake()
    {
        _currentHealth = maxHealth;
        _facingDir = standFacingDir;
        _currentPos = transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        CalcFacingDir();
    }

    private void CalcFacingDir()
    {
        var lastPos = _currentPos;
        _currentPos = transform.position;

        var diff = (_currentPos - lastPos).normalized;
        FacingDirection = diff.magnitude > 0 ? new Vector2(diff.x, diff.z) : standFacingDir;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
    }
}