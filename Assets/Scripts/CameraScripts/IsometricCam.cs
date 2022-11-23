using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class IsometricCam : MonoBehaviour
{
    [SerializeField] private float camDist = 15;

    [Description("The angles of the camera from the player, where the first angle is θ and the second φ")]
    [SerializeField] private Vector2 camAngles = new Vector2(53, 106);

    [SerializeField] private Transform follow;

    private Vector3 _posDir;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         _posDir = Vector3Extension.FromAngles(camAngles.x * Mathf.Deg2Rad, camAngles.y * Mathf.Deg2Rad) * camDist;
         transform.position = follow.position + _posDir;
    }
}
