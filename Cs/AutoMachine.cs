using Godot;
using System;

public partial class AutoMachine : AnimatableBody2D
{
	[Export]
	public MotionType motionType = MotionType.Rotate;
	[Export]
	public float Speed = 1f;
	[Export]
	public float[] PointFloat = [0f, 1f];
	[Export]
	public Vector2[] PointVector = [new(-100f, 0f), new(100f, 0f)];
	private Vector2 StartPos;
	private bool MotionLeft = false;
	public override void _Ready()
	{
		StartPos = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (motionType)
		{
			case MotionType.Rotate:
				Rotation += Speed * (float)delta;
				break;
			case MotionType.PointRotation:
				if (MotionLeft)
				{
					Rotation += Speed * (float)delta;
					if (Rotation > PointFloat[1]) MotionLeft = false;
				}
				else
				{
					Rotation -= Speed * (float)delta;
					if (Rotation < PointFloat[0]) MotionLeft = true;
				}
				break;
			case MotionType.PointLinear:
				if (MotionLeft)
				{
					Position += PointVector[0].Normalized() * Speed * 100f * (float)delta;
					if (Position.DistanceTo(StartPos + PointVector[0]) < 1.0f) MotionLeft = false;
				}
				else
				{
					Position += PointVector[1].Normalized() * Speed * 100f * (float)delta;
					if (Position.DistanceTo(StartPos + PointVector[1]) < 1.0f) MotionLeft = true;
				}
				GD.Print(Position);
				break;
			case MotionType.PointScale:
				if (MotionLeft)
				{
					Scale += new Vector2(Speed * (float)delta, Speed * (float)delta);
					if (Scale.X > PointFloat[1] + 1f) MotionLeft = false;
				}
				else
				{
					Scale -= new Vector2(Speed * (float)delta, Speed * (float)delta);
					if (Scale.X < PointFloat[0] + 1f) MotionLeft = true;
				}
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
