using Godot;
using System;

public partial class StartMessage : Label
{
	Label playerScore;
	Label enemyScore;
	Ball ball;


	public override void _Ready()
	{
		Show();
		Node table = FindParent("Table");
		playerScore = table.GetNode<Label>("Labels/PlayerScore");
		enemyScore = table.GetNode<Label>("Labels/EnemyScore");
		ball = table.GetNode<Ball>("Ball");
	}

	public override void _Process(double delta)
	{
		if (Int32.Parse(playerScore.Text) + Int32.Parse(enemyScore.Text) == 0 && !ball.HasLaunched)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}
}
