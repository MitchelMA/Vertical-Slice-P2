using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : GenericSingleton<Bounds>
{
    [SerializeField] private Vector4 outerBounds = Vector4.zero;
    [SerializeField] private float middleLine = 0f;
#if UNITY_EDITOR
    [SerializeField] private float gizmosHeight = 0f;
#endif // UNITY_EDITOR

    public Vector4 OuterBounds => outerBounds;
    public float MiddleLine => middleLine;

    public Vector4 this[int idx] =>
        idx switch
        {
            0 => RightBounds,
            1 => LeftBounds,
            _ => throw new IndexOutOfRangeException()
        };

    public Vector4 RightBounds => new Vector4(outerBounds.x, outerBounds.y, outerBounds.z, middleLine);
    public Vector4 LeftBounds => new Vector4(outerBounds.x, middleLine, outerBounds.z, outerBounds.w);

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        float height = gizmosHeight;

        Gizmos.color = Color.blue;

        // draw top-line
        Gizmos.DrawLine(new Vector3(outerBounds.w, height, outerBounds.x),
            new Vector3(outerBounds.y, height, outerBounds.x));

        // draw right-line
        Gizmos.DrawLine(new Vector3(outerBounds.y, height, outerBounds.x),
            new Vector3(outerBounds.y, height, outerBounds.z));

        // draw bottom-line
        Gizmos.DrawLine(new Vector3(outerBounds.w, height, outerBounds.z),
            new Vector3(outerBounds.y, height, outerBounds.z));

        // draw left-line
        Gizmos.DrawLine(new Vector3(outerBounds.w, height, outerBounds.x),
            new Vector3(outerBounds.w, height, outerBounds.z));

        Gizmos.color = Color.red;

        // draw middleLine
        Gizmos.DrawLine(new Vector3(middleLine, height, outerBounds.x), new Vector3(middleLine, height, outerBounds.z));
    }
#endif // UNITY_EDITOR
}