using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootDodgeball : MonoBehaviour
{
    public float speed;
    public GameObject[] Targets;
    public int TargetIndex = 0;
    public int dodgeballs = 1;
    public Dodgeball dodgeball;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dodgeballs > 0)
        {
            Debug.Log("shoot");
            Vector3 dir = Targets[TargetIndex].transform.position - transform.position;
            var clone = Instantiate(dodgeball);
            clone.Setup(dir, transform.position, speed);

            dodgeballs -= 1;
        }
    }

}

