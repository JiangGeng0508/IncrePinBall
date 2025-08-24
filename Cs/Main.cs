using Godot;
using System;

public partial class Main : Node2D
{
	public Hud Hud;
	public Spawner Spawner;
	private int bonus = 0;
	public int Bonus
	{
		get { return bonus; }
		set
		{
			bonus = value;
			Hud?.SetBonus(bonus);
		}
	}
	public bool inCoolDown = false;
	public override void _Ready()
	{
		Hud = GetNode<Hud>("HUD");
		Spawner = GetNode<Spawner>("Spawner");
	}
	public void OnBallDropped(Ball ball, string areaType)
	{

		if (areaType == "Dead")
		{
			if (GetTree().GetNodeCountInGroup("Ball") == 0) CallDeferred(nameof(Reload));
		}
		else if (areaType == "Coin") Bonus++;
		else if (areaType == "Bonus")
		{
			if (Bonus <= 0 || inCoolDown) return;
			Bonus--;
			var pos = ball.Position + new Vector2(GD.Randf() * 200 - 100, GD.Randf() * 200 - 100);
			Spawner.CallDeferred(nameof(Spawner.SpawnBall), pos);
			GetTree().CreateTimer(0.5f).Timeout += () => { inCoolDown = false; };
			inCoolDown = true;
		}
	}
	public void Reload()
	{
		GetTree().ReloadCurrentScene();
	}
}
