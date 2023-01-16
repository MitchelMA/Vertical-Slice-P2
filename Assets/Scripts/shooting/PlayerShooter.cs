using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                Shoot(false);
                _walking.CurrentSpeed = _walking.speed;
            }
        }
    }

    public float ChargeTimer
    {
        get => _chargeTimer;
        set
        {
            _chargeTimer = value;
            if (_chargeTimer > chargedValue)
            {
                Shoot(true);
                _walking.CurrentSpeed = _walking.speed;
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _walking = GetComponent<Walking>();
        int other = (int) side;
        other++;
        other %= 2;
        _targets = TeamsData.Instance[(Side)other].Members.ToArray();
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
            ChargeTimer += Time.deltaTime;
    }

    public void UpdateTarget()
    {
        if (!Input.GetKeyDown(targetChange)) return;
        TargetIndex += 1;
    }

    protected bool Shoot(bool fromAutoCharge = false)
    {
        if (fromAutoCharge)
            _isCharging = false;

        return base.Shoot();
    }
}