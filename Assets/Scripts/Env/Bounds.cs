using System;
using UnityEngine;

public class Bounds : GenericSingleton<Bounds>
{
    [SerializeField] private float width = 5f;
    [SerializeField] private float height = 5f;
    [SerializeField] private float middleLine = 0f;
    [SerializeField] private Vector2 centre = Vector2.zero;
#if UNITY_EDITOR
    [SerializeField] private float gizmosHeight = 0f;
#endif // UNITY_EDITOR

    public Vector4 OuterBounds => 
        new Vector4(
            height/2f + centre.y, 
            width/2f + centre.x, 
            -height/2f + centre.y, 
            -width/2f + centre.x
            );
    public float MiddleLine => middleLine;

    public Vector4 this[Side side] =>
        side switch
        {
            Side.Right => RightBounds,
            Side.Left => LeftBounds,
            _ => throw new IndexOutOfRangeException()
        };
    

    public Vector4 RightBounds => new Vector4(OuterBounds.x, OuterBounds.y, OuterBounds.z, middleLine + centre.x);
    public Vector4 LeftBounds => new Vector4(OuterBounds.x, middleLine + centre.x, OuterBounds.z, OuterBounds.w);

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        float height = gizmosHeight;

        Gizmos.color = Color.blue;

        // draw top-line
        Gizmos.DrawLine(new Vector3(OuterBounds.w, height, OuterBounds.x),
            new Vector3(OuterBounds.y, height, OuterBounds.x));

        // draw right-line
        Gizmos.DrawLine(new Vector3(OuterBounds.y, height, OuterBounds.x),
            new Vector3(OuterBounds.y, height, OuterBounds.z));

        // draw bottom-line
        Gizmos.DrawLine(new Vector3(OuterBounds.w, height, OuterBounds.z),
            new Vector3(OuterBounds.y, height, OuterBounds.z));

        // draw left-line
        Gizmos.DrawLine(new Vector3(OuterBounds.w, height, OuterBounds.x),
            new Vector3(OuterBounds.w, height, OuterBounds.z));

        Gizmos.color = Color.red;

        // draw middleLine
        Gizmos.DrawLine(new Vector3(middleLine + centre.x, height, OuterBounds.x), new Vector3(middleLine + centre.x, height, OuterBounds.z));
    }
#endif // UNITY_EDITOR
}