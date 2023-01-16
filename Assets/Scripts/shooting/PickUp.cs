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


    void Start()
    {
        dodgeBall = GetComponent<Dodgeball>();
    }

    

    private void OnTriggerStay(Collider collision)
    {
        if (!pickUpAbleTags.Contains(collision.gameObject.tag) || !dodgeBall.WasDropped ||
            !(dodgeBall.DroppedDuration > 1f)) return;
        
        var shooter = collision.gameObject.GetComponent<Shooter>();
        shooter.BallCount += 1;
        Destroy(gameObject);
    }
}
