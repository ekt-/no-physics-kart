﻿using UnityEngine;

public class GuiScalerByTarget 
{

    public static void AutoResize(int screenWidth, int screenHeight)
    {
        var resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

    public static Rect GetResizedRect(Rect rect)
    {
        Vector2 position = GUI.matrix.MultiplyVector(new Vector2(rect.x, rect.y));
        Vector2 size = GUI.matrix.MultiplyVector(new Vector2(rect.width, rect.height));

        return new Rect(position.x, position.y, size.x, size.y);
    }
}
