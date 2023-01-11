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
    public float ChargedValue = 1f;

    private float _chargeTimer = 0f;
    public float ChargeTimer { 
        get => _chargeTimer;
        private set 
        {
            _chargeTimer = value;
            if(_chargeTimer > ChargedValue)
            {
                // code to automatically shoot after charging for 1s
                Shoot(true);
            }
        } 
    }
    private bool _isCharging = false;

    public TextMeshProUGUI CounterText;
    public GameObject Counter;

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
        CounterText.text = dodgeballs.ToString();
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
            if (prev == true && ChargeTimer <= ChargedValue)
            {
                Shoot(false);
            }
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        ShootKeyHandle();
        UpdateTarget();
    }
    private void FixedUpdate()
    {
        if (dodgeballs < 1)
        {
            Counter.SetActive(false);
        }
        else Counter.SetActive(true);

        CounterText.SetText(dodgeballs.ToString());
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
            if(dodgeballs > 0)
                walking._speed = 0f;
        }

        if (Input.GetKeyUp(ChargeDodgeball))
        {
            IsCharging = false;
        }

        if (IsCharging)
            ChargeTimer += Time.deltaTime;
    }

    private (float, Dodgeball) GetDodgeBall(bool fromAutoCharged = false)
    {
        if (fromAutoCharged)
            return (2, ChargedDodgeball);

        else return (1, dodgeball);
    }

    private void Shoot(bool fromAutoCharged = false)
    {
        if(fromAutoCharged)
            _isCharging = false;

        if (dodgeballs <= 0)
            return;

        // calculate the dir
        Vector3 dir = Targets[TargetIndex].transform.position - transform.position;
        
       

        // setup the clone
        var (speedMult, ball) = GetDodgeBall(fromAutoCharged);
        var clone = Instantiate(ball);
        clone.Setup(dir, transform.position + transform.right, speed * speedMult);
        // reset charge timer
        ChargeTimer = 0;
        walking._speed = 5f;

        // subtract from the dodgeballs
        dodgeballs -= 1;
        CounterText.SetText(dodgeballs.ToString());
    }
}

