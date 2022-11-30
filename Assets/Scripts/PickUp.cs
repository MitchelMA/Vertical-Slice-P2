using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    shootDodgeball ShootDodgeball;
    
    void Start()
    {
        ShootDodgeball = FindObjectOfType<shootDodgeball>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            ShootDodgeball.dodgeballs += 1;
        }
    }
}
