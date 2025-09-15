using Godot;
using System;

public partial class AutoMachine : AnimatableBody2D
{
	[Export]
	public MotionType MotionType = MotionType.Rotate;
	[Export]
	public float Speed = 1f;
	[Export]
	public float[] PointFloat = [0f, 1f];
	[Export]
	public Vector2[] PointVector = [new(-100f, 0f), new(100f, 0f)];
	private Vector2 _startPos;
	private bool _motionLeft = false;
	public override void _Ready()
	{
		_startPos = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (MotionType)
		{
			case MotionType.Rotate:
				Rotation += Speed * (float)delta;
				break;
			case MotionType.PointRotation:
				if (_motionLeft)
				{
					Rotation += Speed * (float)delta;
					if (Rotation > PointFloat[1]) _motionLeft = false;
				}
				else
				{
					Rotation -= Speed * (float)delta;
					if (Rotation < PointFloat[0]) _motionLeft = true;
				}
				break;
			case MotionType.PointLinear:
				if (_motionLeft)
				{
					Position += PointVector[0].Normalized() * Speed * 100f * (float)delta;
					if (Position.DistanceTo(_startPos + PointVector[0]) < 1.0f) _motionLeft = false;
				}
				else
				{
					Position += PointVector[1].Normalized() * Speed * 100f * (float)delta;
					if (Position.DistanceTo(_startPos + PointVector[1]) < 1.0f) _motionLeft = true;
				}
				GD.Print(Position);
				break;
			case MotionType.PointScale:
				if (_motionLeft)
				{
					Scale += new Vector2(Speed * (float)delta, Speed * (float)delta);
					if (Scale.X > PointFloat[1] + 1f) _motionLeft = false;
				}
				else
				{
					Scale -= new Vector2(Speed * (float)delta, Speed * (float)delta);
					if (Scale.X < PointFloat[0] + 1f) _motionLeft = true;
				}
				break;
			default:
				break;
		}
	}
}
public enum MotionType
{
	Rotate,
	PointRotation,
	PointLinear,
	PointScale
}
