using Godot;
using System;

public partial class Spawner : MultiplayerSpawner
{
	[Export]
	PackedScene BallScene;
	[Export]
	PackedScene CoinScene;

	public void SpawnBall(Vector2 position)
	{
		var ball = BallScene.Instantiate<Ball>();
		ball.Position = position;
		ball.LinearVelocity = new Vector2(0, -100);
		GetParent().AddChild(ball);
	}

	public void SpawnCoin(Vector2 position)
	{
		var coin = CoinScene.Instantiate<DetectArea>();
		coin.Position = position;
		GetParent().AddChild(coin);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left)
		{
			SpawnBall(GetViewport().GetMousePosition());
		}
	}
}
