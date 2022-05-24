using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugDraw
{
    public static void Box(Vector2 centerPosition, Vector2 direction, float width, float height, Color color, float duration)
    {
        Vector2 upVector = Vector2.Perpendicular(direction);
        Vector2 horizontalOffset = direction * width / 2;
        Vector2 verticalOffset = upVector * height / 2;

        Debug.DrawLine(centerPosition - horizontalOffset + verticalOffset, centerPosition + horizontalOffset + verticalOffset, color, duration);
        Debug.DrawLine(centerPosition - horizontalOffset - verticalOffset, centerPosition + horizontalOffset - verticalOffset, color, duration);
        Debug.DrawLine(centerPosition - horizontalOffset + verticalOffset, centerPosition - horizontalOffset - verticalOffset, color, duration);
        Debug.DrawLine(centerPosition + horizontalOffset + verticalOffset, centerPosition + horizontalOffset - verticalOffset, color, duration);
    }

}
