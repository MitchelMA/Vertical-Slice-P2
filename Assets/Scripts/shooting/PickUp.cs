using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dodgeball))]
public class PickUp : MonoBehaviour
{

    Dodgeball dodgeBall;
    public string playerTag;
    public string enemyTag;


    void Start()
    {
        dodgeBall = GetComponent<Dodgeball>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if ((!collision.gameObject.CompareTag(playerTag) && !collision.gameObject.CompareTag(enemyTag))
            || !dodgeBall.WasDropped) return;
        
        var shooter = collision.gameObject.GetComponent<Shooter>();
        Debug.Log("hit");
        shooter.BallCount += 1;
        Destroy(gameObject);
    }
}
