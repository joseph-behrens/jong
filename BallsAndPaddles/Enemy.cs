using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;



public partial class Enemy : CharacterBody2D
{
    [Export]
    int maxSpeed = 1000;
    [Export]
    int acceleration = 2000;
    [Export]
    int friction = 2000;
    Ball ball;
    Vector2 initialPosition;
    Vector2 direction;
    float modifier;
    
    public Vector2 InitialPosition { get; private set; }


    class DifficultyLevel
    {
        public static float Hard { get; } = 0.15F;
        public static float Normal { get; } = 0.125F;
        public static float Easy { get; } = 0.085F;
    }

    public override void _Ready()
    {
        InitialPosition = Position;
        ball = FindParent("Table").GetNodeOrNull<Ball>("Ball");
        modifier = GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.y / 2;
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputVector = Vector2.Zero;

        if (ball is not null && (ball.Position.y != Position.y) && ball.LinearVelocity != Vector2.Zero)
        {
            inputVector = Position;
            inputVector.y = Position.y + modifier;
            direction = ball.Position - inputVector;
            direction.x = 0;
        }
        else
        {
            direction = Vector2.Zero;
        }
        MoveAndCollide(direction * DifficultyLevel.Normal);
	}

    public void Reset()
    {
        Position = InitialPosition;
        Velocity = Vector2.Zero;
    }
}
