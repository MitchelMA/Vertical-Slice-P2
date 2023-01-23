using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Dodgeball))]
public class PickUp : MonoBehaviour
{
    private Dodgeball dodgeBall;
    public string[] pickUpAbleTags = new string[2];
    private bool _pickedUp = false;


    void Start()
    {
        dodgeBall = GetComponent<Dodgeball>();
    }

    private void OnTriggerStay(Collider other)
    {
       HandleAny(other.gameObject); 
    }

    private void OnCollisionStay(Collision collision)
    {
       HandleAny(collision.gameObject); 
    }

    private void HandleAny(GameObject hitObj)
    {
        if (_pickedUp) return;
        
        if (!pickUpAbleTags.Contains(hitObj.tag) || !dodgeBall.WasDropped ||
            !(dodgeBall.DroppedDuration > 1f)) return;
         
         _pickedUp = true;
         var shooter = hitObj.GetComponent<Shooter>();
         shooter.BallCount += 1;
         Destroy(gameObject);
    }
}
