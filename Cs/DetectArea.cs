using System;
using Godot;

public partial class DetectArea : Area2D
{
	[Export(PropertyHint.Enum, "Dead,Coin,Bonus")]
	public Strign Type { get; set; } = Strign.Dead;
	[Signal]
	public delegate void BallDetectedEventHandler(Ball ball, string type);
	public override void _Ready()
	{
		var main = GetTree().GetFirstNodeInGroup("Main") as Main;
		BallDetected += main.OnBallDropped;
	}
	public void OnBodyEntered(Node2D body)
	{
		if (body is Ball ball)
		{
			if (Type == Strign.Dead)
			{
				ball.RemoveFromGroup("Ball");
				ball.QueueFree();
			}
			EmitSignal(nameof(BallDetected), ball, Type.ToString());
		}
	}
}
public enum Strign
{
	Dead,
	Coin,
	Bonus
} 
