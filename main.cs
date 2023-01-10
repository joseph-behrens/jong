using Godot;
using System;

public partial class main : Node2D
{
    [Export]
    int winningScore = 11;
    Enemy enemyPaddle;
    Ball ball;
    Player playerPaddle;
    bool gameHasEnded = false;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemyPaddle = GetNode<Enemy>("Paddles/Enemy");
        playerPaddle = GetNode<Player>("Paddles/Player");
        ball = GetNode<Ball>("Ball");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("launch") && !ball.HasLaunched)
        {
            ball.Launch();
        }

        if (!ball.HasLaunched)
        {
            Vector2 inputValue = Vector2.Zero;
            inputValue.y = playerPaddle.Position.y + 50;
            inputValue.x = playerPaddle.Position.x + 18;
            ball.Position = inputValue;
        }

        if (gameHasEnded)
        {
            if (Input.IsActionJustPressed("restart"))
            {
                StartNewGame();
            }
        }
    }

    private void StartNewGame()
    {
        ball.Reset();
        enemyPaddle.Reset();
        playerPaddle.Reset();
        GetNode<Label>("Labels/PlayerScore").Text = "0";
        GetNode<Label>("Labels/EnemyScore").Text = "0";
        GetNode<Label>("Labels/GameOver").Hide();
        gameHasEnded = false;
    }

    public void BallEnteredPlayerGoal(Node2D body)
    {
        UpdateScore("Labels/EnemyScore");
    }

    public void BallEnteredEnemyGoal(Node2D body)
    {
        UpdateScore("Labels/PlayerScore");
    }

    private void UpdateScore(string scoreToUpdate)
    {
        Ball ball = GetNodeOrNull<Ball>("Ball");
        Label scoreLabel = GetNodeOrNull<Label>(scoreToUpdate);
        int currentScore = Int32.Parse(scoreLabel.Text);
        scoreLabel.Text = (currentScore + 1).ToString();
        if (currentScore + 1 == winningScore)
        {
            Label gameOver = GetNode<Label>("Labels/GameOver");
            if (scoreToUpdate == "Labels/PlayerScore")
            {
                gameOver.Text = "Game Over\nYou won!!";
            }
            else
            {
                gameOver.Text = "Game Over\nYou Lost...";
            }
            gameOver.Text += "\n\npress enter to play again";
            gameOver.Show();
            gameHasEnded = true;
        }
        else
        {
            ball.Reset();
            enemyPaddle.Reset();
        }
    }
}
