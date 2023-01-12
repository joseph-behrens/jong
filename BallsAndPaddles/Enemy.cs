using Godot;
using System;
using System.Drawing;

public partial class Enemy : CharacterBody2D
{
    [Export]
    int maxSpeed = 2000;
    [Export]
    int acceleration = 2000;
    [Export]
    int friction = 2000;
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

        // TODO: Find a better way to do this. There's jitter when the ball is coming straight and
        //          It also sometimes doesn't quite catch the bounce properly
        if (ball is not null && (ball.Position.y != Position.y) && ball.LinearVelocity != Vector2.Zero)
        {
            inputVector.y = ball.Position.y;
            float comparison = Position.y + GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.y / 2;
            if (ball.Position.y < comparison)
            {
                Velocity = Velocity.MoveToward(-inputVector, acceleration * (float)delta);
            }
            else
            {
                Velocity = Velocity.MoveToward(inputVector, acceleration * (float)delta);
            }
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
