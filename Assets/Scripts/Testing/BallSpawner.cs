using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Dodgeball ball;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 dir = target.position - transform.position;
            var clone = Instantiate(ball);
            clone.Setup(dir, transform.position, speed);
        }
    }
}
