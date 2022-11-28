using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCam : MonoBehaviour
{
    /// <summary>
    /// Settings of the camera.
    /// Where x = distance;
    /// y = phi angle;
    /// z = theta angle
    /// </summary>
    [SerializeField] private Vector2 camSettings = new Vector2(10, 60);
    [SerializeField] private Vector3 focusPoint;
    
    private Vector3 _posDir = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // set the camera at the correct position
        _posDir = Vector3Extensions.FromSpherical(camSettings.x, camSettings.y * Mathf.Deg2Rad, 1.57079632679f);
        transform.position = focusPoint + _posDir;
        
        // rotate the camera towards the focus-point
        Vector3 targetDir = focusPoint - transform.position;
        transform.rotation = Quaternion.LookRotation(targetDir);
    }
}
