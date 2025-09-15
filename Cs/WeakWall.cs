using Godot;
using System;

public partial class WeakWall : RigidBody2D
{
    public void OnBodyEntered(Node body)
    {
        if (body is Ball)
        {
            GD.Print("WeakWall hit");
        }
    }
}
