using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int _currentHealth;
    private Vector2 _facingDir = Vector2.right;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value == _currentHealth)
                return;
            var prev = _currentHealth;
            _currentHealth = value;
            
            healthChange.Invoke(new HealthChangeData
            {
                Previous = prev,
                Current = _currentHealth,
            });
        }
    }
    
    // Events
    public UnityEvent<HealthChangeData> healthChange = new UnityEvent<HealthChangeData>();

    // Awake is called before Start
    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        CurrentHealth -= 1;
    }

    public void Test(HealthChangeData data)
    {
        print($"{data.Previous}; {data.Current}");
    }
}
