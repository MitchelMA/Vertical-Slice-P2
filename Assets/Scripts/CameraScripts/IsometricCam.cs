using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class IsometricCam : MonoBehaviour
{
    [SerializeField] private float camDist = 15;
    [SerializeField] private float adjustSpeed = 10f;

    [Description("The angles of the camera from the player, where the first angle is θ and the second φ")]
    [SerializeField] private Vector2 camAngles = new Vector2(60, 120);

    [SerializeField] private Transform follows;

    private Vector3 _posDir;

    // Update is called once per frame
    void Update()
    {
         _posDir = Vector3Extension.FromSpherical(camDist, camAngles.x * Mathf.Deg2Rad, camAngles.y * Mathf.Deg2Rad);
         transform.position = follows.position + _posDir;

         Vector3 targetDir = follows.position - transform.position;
         float singleStep = adjustSpeed * Time.deltaTime;
         Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0f);
         Debug.DrawRay(transform.position, newDir, Color.red);
         transform.rotation = Quaternion.LookRotation(newDir);
    }
}
