using Godot;

public partial class UIRect : ColorRect {
    /*
    public UIRect(Rectangle bounds) {
        Position = bounds.TopLeft;
        Size = new Vector2I(bounds.GetWidth(), bounds.GetHeight());
    }
    */

    public void Initialize(Rectangle bounds) {
        Position = bounds.TopLeft;
        Size = new Vector2(bounds.GetWidth(), bounds.GetHeight());
    }
}