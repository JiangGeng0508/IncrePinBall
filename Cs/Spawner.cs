using Godot;
using System;

public partial class Spawner : MultiplayerSpawner
{
	[Export] public PackedScene BallScene;
	[Export] public PackedScene CoinScene;

	public void SpawnBall(Vector2 position)
	{
		var ball = BallScene.Instantiate<Ball>();
		ball.Position = position;
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
		if (@event is InputEventMouseButton mouseButtonEvent )
		{
			if (mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left)
			{
				var position = mouseButtonEvent.GlobalPosition;
				GetNode<Sprite2D>("SpawnNote").GlobalPosition = position;
				SpawnBall(position);
			}
		}
	}
}
