using Godot;
using System;

public partial class Hud : Node
{
	public Label Bonus;
	public override void _Ready()
	{
		Bonus = GetNode<Label>("Bonus");
	}
	public void SetBonus(int bonus)
	{
		Bonus.Text = "Bonus: " + bonus.ToString();
	}
}
