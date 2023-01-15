using Godot;
using System;

public partial class BackButton : Button
{
    public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Table/Menu.tscn");
    }
}
