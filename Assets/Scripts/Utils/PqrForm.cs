using System;
using UnityEngine;

/// <summary>
/// Structure for the linear formula `px + qy = r`
/// </summary>
public struct PqrForm
{
    /// <summary>The p constant in the linear formula `px + qy = r`</summary>
    public float p;

    /// <summary>the q constant in the linear formula `px + qy = r`</summary>
    public float q;

    /// <summary>the r constant in the linear formula `px + qy = r`</summary>
    public float r;

    /// <summary>
    /// Method to get the point (x, 0) where x is an arbitrary number and 0 the y-value.<br/>
    /// This method can also return `null`. When null is returned, the line doesn't intersect with the x-axis.<br/>
    /// This method can also return `+Infinity` meaning that the line has no slope, but also has a `R` value of 0 
    /// </summary>
    /// <returns>
    /// The point where the line intersects with the x-axis
    /// </returns>
    public Vector2? Root
    {
        get
        {
            if (!float.IsInfinity(GetX(0))) return new Vector2(GetX(0), 0);
            if (r == 0)
            {
                return new Vector2(float.PositiveInfinity, 0);
            }

            return null;

        }
    }

    /// <summary>
    /// Method to get the point (0, y) where y is an arbitrary number and 0 is the x-value.<br/>
    /// This method can also return `null`. When null is returned, the line doesn't intersect with the y-axis.<br/>
    /// This method can also return `+Infinity` meaning that the line is equal to `x = 0`.
    /// </summary>
    /// <returns>
    /// The point where the line intersects with the y-axis;
    /// </returns>
    public Vector2? YIcept
    {
        get
        {
            if (!float.IsInfinity(GetY(0))) return new Vector2(0, GetY(0));
            if (r == 0)
            {
                return new Vector2(0, float.PositiveInfinity);
            }

            return null;

        }
    }

    /// <summary>
    /// Gets the slope of the formula
    /// </summary>
    public float Slope
    {
        get
        {
            if (q == 0)
            {
                return float.PositiveInfinity;
            }

            return -p / q;
        }
    }

    /// <summary>
    /// Gets the angle of the slope in radians
    /// </summary>
    public float Angle => Mathf.Atan2(Slope, 1);

    /// <summary>
    /// Constructor to create a line between point a to point b
    /// </summary>
    /// <param name="point1">Point a</param>
    /// <param name="point2">Point b</param>
    public PqrForm(Vector2 point1, Vector2 point2)
    {
        float deltaY = point2.y - point1.y;
        float deltaX = point2.x - point1.x;
        float rc = deltaY / deltaX;

        if (float.IsNaN(rc))
        {
            rc = 0;
        }

        if (!float.IsInfinity(rc))
        {
            float r = point1.y - rc * point1.x;
            p = -rc;
            q = 1;
            this.r = r;
            return;
        }

        p = 1;
        q = 0;
        r = point1.x;
    }

    /// <summary>
    /// Constructor to create a `px + pq = r` formula from a `y = ax + b` formula
    /// </summary>
    /// <param name="a">The a variable in the `y = ax + b` formula</param>
    /// <param name="b">The b variable in the `y = ax + b` formula</param>
    public PqrForm(float a, float b)
    {
        if (float.IsInfinity(a))
        {
            p = 1;
            q = 0;
            r = b;
            return;
        }

        p = -a;
        q = 1;
        r = b;
    }

    /// <summary>
    /// Constructor to create a formula from a slope and an arbitrary point through which the formula should pass
    /// </summary>
    /// <param name="slope">The slope of the formula</param>
    /// <param name="point">The point that should be on the formula</param>
    public PqrForm(float slope, Vector2 point)
    {
        if (float.IsInfinity(slope))
        {
            p = 1;
            q = 0;
            r = point.x;
            return;
        }

        float b = point.y - point.x * slope;
        p = -slope;
        q = 1;
        r = b;
    }

    /// <summary>
    /// Constructor to copy a formula
    /// </summary>
    /// <param name="copy">The formula that gets copied</param>
    public PqrForm(PqrForm copy)
    {
        p = copy.p;
        q = copy.q;
        r = copy.r;
    }

    /// <summary>
    /// Method to get the X value of a formula at the given Y-position
    /// </summary>
    /// <param name="y">Y-position you want to know the X-value of</param>
    /// <returns>
    /// The X-position.<br/>
    /// When the line has a slope of 0, this method will return +Infinity
    /// </returns>
    public readonly float GetX(float y)
    {
        if (p != 0)
        {
            return (r - q * y) / p;
        }

        return float.PositiveInfinity;
    }

    /// <summary>
    /// Method to get the Y value of a formula at the given X-position
    /// </summary>
    /// <param name="x">X-position you want to know the Y-value of</param>
    /// <returns>
    /// The Y-position.<br/>
    /// When the slope of the formula is `Infinity`, this method will return +Infinity
    /// </returns>
    public readonly float GetY(float x)
    {
        if (q != 0)
        {
            return (r - p * x) / q;
        }

        return float.PositiveInfinity;
    }

    /// <summary>
    /// Method to get a slope from a given angle
    /// </summary>
    /// <param name="angle">The angle in radians</param>
    /// <returns>The slope that gets calculated with the given angle</returns>
    public static float SlopeFromAngle(float angle)
    {
        if (Mathf.Abs(Mathf.Abs(angle) - Mathf.PI / 2) < 0.0001f)
        {
            return float.PositiveInfinity;
        }

        return Mathf.Tan(angle);
    }


    /// <summary>
    /// Method to get an angle from a given slope
    /// </summary>
    /// <param name="slope">The given slope</param>
    /// <returns>The angle in radians calculated with the given slope</returns>
    public static float AngleFromSlope(float slope) => Mathf.Atan2(slope, 1);

    /// <summary>
    /// Method to get the formula that will intersect this formula at a right-angle
    /// </summary>
    /// <returns>A formula that intersects with a right-angle</returns>
    public PqrForm Perpendicular()
    {
        return new PqrForm
        {
            p = q,
            q = -p,
            r = r,
        };
    }

    /// <summary>
    /// Method to get the intersection-point of two formula's
    /// </summary>
    /// <param name="form1">Formula 1</param>
    /// <param name="form2">Formula 2</param>
    /// <returns>
    /// The point at which the two formula's intersect
    /// </returns>
    public static Vector2? Intersect(PqrForm form1, PqrForm form2, double tolerance)
    {
        // if clauses to test if the lines actually intersect;
        float slopeDiff = Mathf.Abs(form1.Slope - form2.Slope);
        if (float.IsInfinity(form1.Slope) && float.IsInfinity(form2.Slope))
        {
            slopeDiff = 0;
        }

        if (slopeDiff <= tolerance)
        {
            if (Math.Abs(form1.r / form1.q - form2.r / form2.q) < tolerance)
            {
                return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            }

            return null;
        }

        // the pqr variables of the first formula
        float p = form1.p;
        float q = form1.q;
        float r = form1.r;

        // pqr variables of the second formula
        float a = form2.p;
        float b = form2.q;
        float c = form2.r;
        float x = 0, y = 0;

        if (a != 0 && q != 0 && b == 0)
        {
            x = c / a;
            y = r / q - (p * c) / (q * a);
            return new Vector2(x, y);
        }

        if (b != 0 && p != 0 && a == 0)
        {
            y = c / b;
            x = r / p - (q * c) / (p * b);
            return new Vector2(x, y);
        }

        if ((q != 0 && a != 0 && p == 0) || (p != 0 && b != 0 && q == 0))
        {
            // in this case, it is best to switch the formula's
            return Intersect(form2, form1, tolerance);
        }

        // get the x value
        float xFactor = q / b;
        x = (r - xFactor * c) / (p - xFactor * a);

        // get te y value
        float yFactor = p / a;
        y = (r - yFactor * c) / (q - yFactor * b);

        return new Vector2(x, y);
    }

    /// <summary>
    /// Method to get the string that represents this formula
    /// </summary>
    /// <returns>The string that represents this formula</returns>
    public readonly override string ToString()
    {
        return $"{p}x + {q}y = {r}";
    }
}