using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootDodgeball : MonoBehaviour
{
    public float speed;
    public int dodgeballs = 1;
    public int TargetIndex = 0;
    public float ChargeTimer;

    public GameObject[] Targets;
    public KeyCode ChargeDodgeball;
    
    public Dodgeball dodgeball;
    public Dodgeball ChargedDodgeball;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(ChargeDodgeball) && dodgeballs > 0)
        {
            ChargeTimer += Time.deltaTime;
        }
        if (Input.GetKeyUp(ChargeDodgeball) && dodgeballs > 0 && ChargeTimer > 2)
        {
            Debug.Log("shoot");
            Vector3 dir = Targets[TargetIndex].transform.position - transform.position;
            var clone = Instantiate(ChargedDodgeball);
            clone.Setup(dir, transform.position, speed * 2);

            dodgeballs -= 1;
        }
        if (Input.GetKeyUp(ChargeDodgeball) && dodgeballs > 0 && dodgeballs < 2)
        {
            Vector3 dir = Targets[TargetIndex].transform.position - transform.position;
            var clone = Instantiate(dodgeball);
            clone.Setup(dir, transform.position, speed);
            dodgeballs -= 1;
        }
    }

}

