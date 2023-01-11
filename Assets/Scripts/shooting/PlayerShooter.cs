using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Walking))]
public class PlayerShooter : Shooter
{
    [SerializeField] private KeyCode targetChange = KeyCode.F;
    private Walking _walking;

    public bool IsCharging
    {
        get => _isCharging;
        set
        {
            if (value == _isCharging)
                return;

            var prev = _isCharging;
            _isCharging = value;

            if (prev)
            {
                Shoot();
                chargeTimer = 0;
                _walking.CurrentSpeed = _walking.speed;
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _walking = GetComponent<Walking>();
    }


    // Update is called once per frame
    private void Update()
    {
        ShootKeyHandle();
        UpdateTarget();
    }

    public void ShootKeyHandle()
    {
        if (Input.GetKeyDown(shootKey))
        {
            IsCharging = true;
            _walking.CurrentSpeed = 0f;
        }

        if (Input.GetKeyUp(shootKey))
        {
            IsCharging = false;
        }

        if (IsCharging)
            chargeTimer += Time.deltaTime;
    }

    public void UpdateTarget()
    {
        if (!Input.GetKeyDown(targetChange)) return;
        targetIndex += 1;
        targetIndex %= _targets.Length;
        print(targetIndex);
    }
}