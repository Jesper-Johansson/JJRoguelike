using System;
using Godot;

public class Rectangle {
    public Vector2I TopLeft;
    public Vector2I BottomRight;

    public Rectangle(Vector2I topLeft, Vector2I bottomRight) {
        if (BottomRight.X < TopLeft.X || BottomRight.Y < TopLeft.Y) throw new Exception("Rectangle corners are invalid!");
        this.TopLeft = topLeft;
        this.BottomRight = bottomRight;
    }

    public int GetWidth() {
        return
            Math.Abs(TopLeft.X - BottomRight.X) + 1;
    }
    public int GetHeight() {
        return
            Math.Abs(TopLeft.Y - BottomRight.Y) + 1;
    }
}