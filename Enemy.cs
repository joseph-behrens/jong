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
    Ball ball;
    Vector2 initialPosition;

    public Vector2 InitialPosition { get; private set; }

    public override void _Ready()
    {
        InitialPosition = Position;
        ball = FindParent("Table").GetNodeOrNull<Ball>("Ball");
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputVector = Vector2.Zero;

        if (ball is not null && (ball.Position.y != Position.y))
        {
            inputVector.y = ball.LinearVelocity.y;
            Velocity = Velocity.MoveToward(inputVector, acceleration * (float)delta);
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
        Velocity = Vector2.Zero;
    }
}
