using Godot;
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
		//velocity = new Vector2(maxSpeed, random.Next(-maxSpeed, maxSpeed));
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

	public void OnPlayerWallAreaEntered(Node2D body)
	{
		GD.Print("Ball entered player wall");
		QueueFree();
		// Update score for enemy
		// Check for winner
		// Launch new ball
	}
}
