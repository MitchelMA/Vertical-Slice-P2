using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootDodgeball : MonoBehaviour
{
    public float speed;
    public int dodgeballs = 1;
    [SerializeField] private int targetIndex = 0;
    public float ChargeTimer;
    private bool _isCharging = false;

    public GameObject[] Targets;
    public KeyCode ChargeDodgeball;
    
    public Dodgeball dodgeball;
    public Dodgeball ChargedDodgeball;

    public GameObject[] bars;
    
    public int TargetIndex
    {
        get => targetIndex;
        private set
        {
            if (value == targetIndex)
                return;

            targetIndex = value;

            // doesn't go beyond length
            targetIndex %= Targets.Length;
            // doesn't go below zero
            if(targetIndex < 0)
                targetIndex = Targets.Length - 1;

            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].SetActive(false);
            }
            bars[targetIndex].SetActive(true);
        }
    }

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
        if (ChargeTimer > 1f)
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

