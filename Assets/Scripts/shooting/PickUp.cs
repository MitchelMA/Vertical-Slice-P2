using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Dodgeball))]
public class PickUp : MonoBehaviour
{
    private Dodgeball dodgeBall;
    public string[] PickUpAbleTags;


    void Start()
    {
        dodgeBall = GetComponent<Dodgeball>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if ((!PickUpAbleTags.Contains(collision.gameObject.tag))
            || !dodgeBall.WasDropped) return;
        
        var shooter = collision.gameObject.GetComponent<Shooter>();
        Debug.Log("hit");
        shooter.BallCount += 1;
        Destroy(gameObject);
    }
}
