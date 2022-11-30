using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootDodgeball : MonoBehaviour
{
    public float speed;
    public int dodgeballs = 1;
    public int TargetIndex = 0;
    public float ChargeTimer;
    private bool _isCharging = false;

    public GameObject[] Targets;
    public KeyCode ChargeDodgeball;
    
    public Dodgeball dodgeball;
    public Dodgeball ChargedDodgeball;

    public bool IsCharging
    {
        get => _isCharging;
        private set
        {
            if (value == _isCharging)
                return;

            var prev = _isCharging;
            _isCharging = value;

            // if prev is *true*, _isCharging is automatically *false*
            if (prev == true)
            {
                Shoot();
                // reset charge timer
                ChargeTimer = 0;
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        ShootKeyHandle();
        UpdateTarget();
    }

    public void UpdateTarget()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            TargetIndex += 1;
            // doesn't go beyond length
            TargetIndex %= Targets.Length;
            Debug.Log(TargetIndex);
        }
    }
    public void ShootKeyHandle()
    {
        if (Input.GetKeyDown(ChargeDodgeball))
        {
            IsCharging = true;
        }

        if (Input.GetKeyUp(ChargeDodgeball))
        {
            IsCharging = false;
        }

        if (IsCharging)
            ChargeTimer += Time.deltaTime;
    }

    private (float, Dodgeball) GetDodgeBall()
    {
        if (ChargeTimer > 2f)
            return (2, ChargedDodgeball);

        else return (1, dodgeball);
    }

    private void Shoot()
    {
        if (dodgeballs <= 0)
            return;

        // calculate the dir
        Vector3 dir = Targets[TargetIndex].transform.position - transform.position;

        // setup the clone
        var (speedMult, ball) = GetDodgeBall();
        var clone = Instantiate(ball);
        clone.Setup(dir, transform.position, speed * speedMult);

        // subtract from the dodgeballs
        dodgeballs -= 1;
    }
}

