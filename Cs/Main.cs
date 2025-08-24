using Godot;
using System;

public partial class Main : Node2D
{
	public Hud Hud;
	public int Bonus = 0;
	public override void _Ready()
	{
		Hud = GetNode<Hud>("HUD");
	}
	public void OnBallDropped(Ball ball, string areaType)
	{
		if (areaType == "Dead")
		{
			if (GetTree().GetNodeCountInGroup("Ball") == 0)
			{
				CallDeferred(nameof(Reload));
			}
		}
		else if (areaType == "Coin")
		{
			Bonus++;
			Hud.SetBonus(Bonus);
		}
	}
	public void Reload()
	{
		GetTree().ReloadCurrentScene();
	}
}
