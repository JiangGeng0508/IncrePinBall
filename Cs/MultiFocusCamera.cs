using Godot;
using System;

public partial class MultiFocusCamera : Camera2D
{
	public override void _Process(double delta)
	{
		var points = GetTree().GetNodesInGroup("FocusPoint");
		var position = new Vector2(640,360);
		foreach (var point in points)
		{
			if (point is Node2D node)
			position += node.GlobalPosition;
		}
		position /= points.Count + 1;
		Position = position;
	}
}
