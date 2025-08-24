using Godot;
using System;

public partial class BounceBoard : AnimatableBody2D
{
	private Tween tween;
	[Export]
	public actionKey ActionKey = actionKey.BounceL;
	private StringName curing;
	[Export]
	public float curingDuration = 0.03f;
	[Export]
	public float curingDegree = Mathf.Pi / 6;
	private float curingRotation;
	private float initRotation;
	public override void _Ready()
	{
		switch (ActionKey)
		{
			case actionKey.BounceL:
				curing = "BounceL";
				curingRotation = Rotation - curingDegree;
				break;
			case actionKey.BounceR:
				curing = "BounceR";
				curingRotation = Rotation + curingDegree;
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
			tween?.Kill();
			tween = CreateTween();
			tween.TweenProperty(this, "rotation", curingRotation, curingDuration);
			tween.Play();
		}
		else if (@event.IsActionReleased(curing))
		{
			tween?.Kill();
			tween = CreateTween();
			tween.TweenProperty(this, "rotation", initRotation, 0.1f);
			tween.Play();
		}
	}
}
public enum actionKey
{
	BounceL,
	BounceR,
}
