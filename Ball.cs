using Godot;
using Godot.Collections;
using System;

public partial class Ball : RigidBody2D
{
	[Export]
	int maxSpeed = 500;
	Vector2 windowSize;
	Vector2 velocity;
	Vector2 initialPosition;
	bool hasLaunched = false;

    public bool HasLaunched { get => hasLaunched; private set => hasLaunched = value; }
	public Vector2 InitialPoition { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        InitialPoition = Position;
		Reset();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		KinematicCollision2D collisionInfo = MoveAndCollide(LinearVelocity * (float)delta);
		if (collisionInfo != null) 
		{
            LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal());
			var colliderId = collisionInfo.GetColliderId();
			var playerId = FindParent("Table").GetNode<Player>("Paddles/Player").GetInstanceId();
            if ((Input.IsActionPressed("ui_up") || Input.IsActionPressed("ui_down")) && colliderId == playerId)
			{
				LinearVelocity *= 1.5f;
				AngularVelocity *= 1000F;
            }
			
			GD.Print(LinearVelocity.ToString());
		}
	}

	public void Reset()
    {
		HasLaunched = false;
		LinearVelocity = Vector2.Zero;
        Position = InitialPoition;
    }

	public void Launch()
    {
        HasLaunched = true;
        var random = new Random();
        LinearVelocity = new Vector2(maxSpeed, random.Next(-maxSpeed, maxSpeed));
    }
}
