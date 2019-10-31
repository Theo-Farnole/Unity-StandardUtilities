using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Texture2DExtension
{
    public static Sprite ToSprite(this Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
