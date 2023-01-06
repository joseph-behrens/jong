using Godot;
using Godot.Collections;
using System;

public partial class Ball : RigidBody2D
{
	[Export]
	int maxSpeed = 500;
	Vector2 windowSize;
	Vector2 velocity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        var random = new Random();
        LinearVelocity = new Vector2(maxSpeed, random.Next(-maxSpeed, maxSpeed));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		KinematicCollision2D collisionInfo = MoveAndCollide(LinearVelocity * (float)delta);
		if (collisionInfo != null) 
		{
			LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal());
		}
	}

	public void BallEnteredPlayerGoal(Node2D body)
	{
		updateScore("PlayerScore");
	}

    public void BallEnteredEnemyGoal(Node2D body)
    {
		updateScore("EnemyScore");
    }

	private void updateScore(string scoreToUpdate)
	{
		QueueFree();
		Label scoreLabel = GetParent().GetNodeOrNull<Label>(scoreToUpdate);
		int currentScore = Int32.Parse(scoreLabel.Text);
		scoreLabel.Text =  (currentScore + 1).ToString();
        // Check for winner
        // Launch new ball
    }
}
