using Godot;



public partial class Enemy : CharacterBody2D
{
    int acceleration = 2000;
    int friction = 2000;
    int speedModifier = 20000;

    Properties propertiesValues;
    int maxSpeed;
    Ball ball;
    Vector2 initialPosition;
    Vector2 direction;
    float positionModifier;
    
    public Vector2 InitialPosition { get; private set; }


    public override void _Ready()
    {
        propertiesValues = GetNode<Properties>("/root/Properties");
        maxSpeed = (int)propertiesValues.cpuPaddleSpeed;
        InitialPosition = Position;
        ball = FindParent("Table").GetNodeOrNull<Ball>("Ball");
        positionModifier = GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.y / 2;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputVector = Vector2.Zero;

        if (ball is not null && (ball.Position.y != Position.y) && ball.LinearVelocity != Vector2.Zero)
        {
            inputVector = Position;
            inputVector.y = Position.y + positionModifier;
            direction = ball.Position - inputVector;
            direction.x = 0;
        }
        else
        {
            direction = Vector2.Zero;
        }
        MoveAndCollide(direction * maxSpeed/speedModifier);
	}

    public void Reset()
    {
        Position = InitialPosition;
        Velocity = Vector2.Zero;
    }
}
