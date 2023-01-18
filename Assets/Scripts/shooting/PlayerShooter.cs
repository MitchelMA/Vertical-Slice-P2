using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Walking))]
public class PlayerShooter : Shooter
{
    [SerializeField] private KeyCode shootKey = KeyCode.E;
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
        shootEnd.AddListener(ShootEnd);
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

        StartCoroutine(base.Shoot());
        return true;
    }

    private void ShootEnd()
    {
        _walking.CurrentSpeed = _walking.speed;
    }
}