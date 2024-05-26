using Godot;
using System;
using System.Collections.Generic;

public partial class UserInterface : Control
{
    [Export]
    private PackedScene UIRectScene;
    List<Control> elements = new List<Control>();
    public void AddBounds(Rectangle bounds) 
    {
        //elements.Add(new UIRect(bounds));
        //UIRect rect = UIRectScene.Instantiate<UIRect>();
        //rect.Initialize(bounds);
        //UIRect rect = new UIRect(bounds);
        UIRect rect = UIRectScene.Instantiate() as UIRect;
        rect.Initialize(bounds);
        //elements.Add(rect);
        AddChild(rect);
    }
}