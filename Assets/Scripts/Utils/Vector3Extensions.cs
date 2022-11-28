using System;
using System.Collections;
using System.Collections.Generic;
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
}
