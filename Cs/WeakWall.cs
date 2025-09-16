using Godot;
using System;

public partial class WeakWall : StaticBody2D
{
    [Export] public int Health = 3;
    private int _health;
    public override void _Ready()
    {
        _health = Health;
    }

    public void OnBodyEntered(Node body)
    {
        if (body is Ball)
        {
            //脆弱的墙壁被撞击
            _health--;
            GD.Print("WeakWall hitted");
            if (_health <= 0)
            {
                CallDeferred(nameof(Destroy));
            }
            else if (_health < Health)
            {
                GetNode<ColorRect>("ColorRect").Color = new Color((float)_health / Health,0,0,1);
            }
            else
            {
                GetNode<ColorRect>("ColorRect").Color = Colors.White;
            }
        }
    }

    private void Destroy() => QueueFree();
}
