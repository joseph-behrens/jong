using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	int maxSpeed = 1000;
	[Export]
	int acceleration = 1000;
	[Export]
	int friction = 1000;
    public Vector2 InitialPosition { get; private set; }

    public override void _Ready()
    {
		InitialPosition = Position;
    }
    public override void _PhysicsProcess(double delta)
	{
		Vector2 inputVector = Vector2.Zero;
		inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		inputVector = inputVector.Normalized();
		if (inputVector != Vector2.Zero)
		{
            Velocity = Velocity.MoveToward(inputVector * maxSpeed, acceleration * (float)delta);
        }
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, friction * (float)delta);
		}	
        MoveAndSlide();
    }

	public void Reset()
	{
		Position = InitialPosition;
	}
}
