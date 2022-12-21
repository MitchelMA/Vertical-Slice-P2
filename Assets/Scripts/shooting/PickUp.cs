using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    shootDodgeball ShootDodgeball;
    Dodgeball dodgeBall;
    public string playerTag;


    void Start()
    {
        ShootDodgeball = FindObjectOfType<shootDodgeball>();
        dodgeBall = FindObjectOfType<Dodgeball>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == playerTag && dodgeBall.WasDropped == true)
        {
            ShootDodgeball.dodgeballs += 1;
            Destroy(gameObject);
        }
    }
}
