using Godot;
using System;

public partial class BounceBoard : AnimatableBody2D
{
	private Tween tween2;
	private Tween tween1;
	[Export]
	public actionKey ActionKey = actionKey.BounceL;
	private StringName curing;
	private float curingRotation;
	private float initRotation;
	public override void _Ready()
	{
		switch (ActionKey)
		{
			case actionKey.BounceL:
				curing = "BounceL";
				curingRotation = Rotation - Mathf.Pi / 6;
				break;
			case actionKey.BounceR:
				curing = "BounceR";
				curingRotation = Rotation + Mathf.Pi / 6;
				break;
			default:
				break;
		}
		initRotation = Rotation;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed(curing))
		{
			tween1?.Kill();
			tween1 = CreateTween();
			tween1.TweenProperty(this, "rotation", curingRotation, 0.03f);
			tween1.Play();
		}
		else if (@event.IsActionReleased(curing))
		{
			tween2?.Kill();
			tween2 = CreateTween();
			tween2.TweenProperty(this, "rotation", initRotation, 0.1f);
			tween2.Play();
		}
	}
}
public enum actionKey
{
	BounceL,
	BounceR,
}
