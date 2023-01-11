using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
public class shootDodgeball : MonoBehaviour
{
    public Walking walking;
    public float speed;
    public int dodgeballs;
    public int TargetIndex = 0;
    public float ChargeTimer;
    private bool _isCharging = false;

    public TextMeshProUGUI Counter;

    public GameObject[] Targets;
    public KeyCode ChargeDodgeball;
    // public GameObject SpawnPoint;

    private BoxCollider _collider;
    
    public Dodgeball dodgeball;
    public Dodgeball ChargedDodgeball;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        walking = GetComponent<Walking>();   
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
                walking._Speed = 5f;
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
            walking._Speed = 0f;
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
        print(dir);

        // setup the clone
        var (speedMult, ball) = GetDodgeBall();
        var clone = Instantiate(ball);
        clone.Setup(dir, transform.position + transform.right, speed * speedMult); 

        // subtract from the dodgeballs
        dodgeballs -= 1;
        Counter.text = dodgeballs.ToString();
    }
}

