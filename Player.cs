using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	int maxSpeed = 100;
	[Export]
	int acceleration = 500;
	[Export]
	int friction = 500;
	Vector2 size;
	Vector2 windowSize;

	public override void _Ready()
	{
		size = GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size;
        windowSize = GetViewport().GetVisibleRect().Size;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 inputVector = Vector2.Zero;
        float bottomEdgePosition = Position.y + size.y;

		if ((Input.IsActionPressed("ui_down") && bottomEdgePosition > windowSize.y) ||
			(Input.IsActionPressed("ui_up") && Position.y < 0))
		{
			Velocity = Vector2.Zero;
		}
		else
		{
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
    }
}
