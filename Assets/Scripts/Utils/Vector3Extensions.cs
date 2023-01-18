using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Create a Vector3 from the angles Theta and Phi.
    /// Where Theta is the angle from the x-axis to the y-axis and
    /// Phi the angle from the xy-plane to the z-axis.
    /// </summary>
    /// <param name="r">Length of Vector</param>
    /// <param name="phi">Angle in radians</param>
    /// <param name="theta">Angle in radians</param>
    /// <returns>The corresponding Vector3 of the given angles</returns>
    public static Vector3 FromSpherical(float r, float phi, float theta)
    {
        float x = r * Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = r * Mathf.Sin(phi) * Mathf.Sin(theta);
        float z = r * Mathf.Cos(phi);

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Calculates the input vector rotated by the specified angle in radians
    /// over the given axis
    /// </summary>
    /// <param name="vec"></param>
    /// <param name="radians"></param>
    /// <param name="axis"></param>
    /// <returns>A new instance of a Vector3 rotated by the given angle in radians</returns>
    /// <exception cref="IndexOutOfRangeException">When the given axis is not x, y or z</exception>
    public static Vector3 RotatedBy(this Vector3 vec, float radians, char axis)
    {
        if (axis < 'x' || axis > 'z')
            throw new IndexOutOfRangeException("given axis wasn't x, y or z");

        int idx = axis - 'x';
        Vector3 ret = vec;
        switch (idx)
        {
            // x-axis
            case 0:
            {
                ret.y = Mathf.Cos(radians) * vec.y + -Mathf.Sin(radians) * vec.z;
                ret.z = Mathf.Sin(radians) * vec.y + Mathf.Cos(radians) * vec.z;
            }
                break;

            // y-axis
            case 1:
            {
                ret.x = Mathf.Cos(radians) * vec.x + Mathf.Sin(radians) * vec.z;
                ret.z = -Mathf.Sin(radians) * vec.x + Mathf.Cos(radians) * vec.z;
            }
                break;

            // z-axis
            case 2:
            {
                ret.x = Mathf.Cos(radians) * vec.x + -Mathf.Sin(radians) * vec.y;
                ret.y = Mathf.Sin(radians) * vec.x + Mathf.Cos(radians) * vec.y;
            }
                break;
        }

        return ret;
    }

    public static bool IsInBounds(this Vector3 vec3, Vector4 bounds) =>
        (vec3.x > bounds.w && vec3.x < bounds.y) && (vec3.z > bounds.z && vec3.z < bounds.x);
}