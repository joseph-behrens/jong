using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    [Export]
    int maxSpeed = 1000;
    [Export]
    int acceleration = 500;
    [Export]
    int friction = 500;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputVector = Vector2.Zero;
        RigidBody2D ball = GetParent().GetNode<RigidBody2D>("Ball");
        if (ball.Position.y != Position.y)
        {
            GD.Print(ball.Position.y);
            GD.Print(ball.LinearVelocity);
            inputVector.y = ball.LinearVelocity.y;
            Velocity = Velocity.MoveToward(inputVector, acceleration * (float)delta);
        }
        else
        {
            Velocity = Velocity.MoveToward(Vector2.Zero, friction * (float)delta);
        }
        MoveAndSlide();
	}
}
