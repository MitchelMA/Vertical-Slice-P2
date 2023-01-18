using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private float height = 1f;

    private Camera facingCamera;

    private float _alphaAngle = 0;
    private float _betaAngle = 0;

    private void Start()
    {
        facingCamera = GameCam.Instance.Cam;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 offset = PosOffset();
        transform.localPosition = new Vector3(0, offset.y);
        transform.eulerAngles = new Vector3(_alphaAngle * Mathf.Rad2Deg, 0, 0);
    }

    // Returns the alpha angle in radians
    private void SetAlphaAngleFromCam()
    {
        Vector3 camForward = facingCamera.transform.forward;
        Vector3 vectorC = camForward.RotatedBy(-90 * Mathf.Deg2Rad, 'x');

        _alphaAngle = Vector3.Angle(vectorC, Vector3.up) * Mathf.Deg2Rad;
    }
    
    private Vector2 PosOffset()
    {
        SetAlphaAngleFromCam();
        const float pi12 = 1.570796f; 
        // Since the Gamma-angle is 90°, we can deduce that the beta angle is 90 - the Alpha-angle
        _betaAngle = pi12 - _alphaAngle;
        float sideB = height;

        // Using the sinus-rule
        float hypotenuse = sideB / Mathf.Sin(_betaAngle);
        float moveDist = sideB - hypotenuse;
        
        return new Vector2(Mathf.Cos(_betaAngle) * moveDist, Mathf.Sin(_betaAngle) * moveDist);
    }
}