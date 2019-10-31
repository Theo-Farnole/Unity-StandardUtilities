using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension
{
    public static string ToHex(this Color c)
    {
        string hexR = ((int)(c.r * 255)).ToString("X2");
        string hexG = ((int)(c.g * 255)).ToString("X2");
        string hexB = ((int)(c.b * 255)).ToString("X2");

        return hexR + hexG + hexB;
    }

    public static void SetAlpha(this Color c, float alpha)
    {
        c = new Color(c.r, c.g, c.b, alpha);
    }
}
