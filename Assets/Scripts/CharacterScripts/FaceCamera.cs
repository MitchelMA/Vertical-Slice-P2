using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private Camera facingCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TowardsCam();
    }

    // Returns the alpha angle in radians
    private float AlphaAngleFromCam()
    {
        Vector3 camForward = facingCamera.transform.forward;
        Vector3 vectorC = camForward.RotatedBy(-90 * Mathf.Deg2Rad, 'x');

        return Vector3.Angle(vectorC, Vector3.up) * Mathf.Deg2Rad;
    }

    private void TowardsCam()
    {
        float alphaAngle = AlphaAngleFromCam();
        // since the Gamma-angle is 90°, we can deduce that the beta angle is 90 - the Alpha-angle
        float betaAngle = 90 - alphaAngle;

        float height = transform.localScale.y;

    }
}