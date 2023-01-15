using Godot;
using System;


// TODO: Keep track of matches won/lost and total points
// TODO: Allow save of stats
// TODO: Add two player mode
public partial class main : Node2D
{
    bool gameHasEnded = false;

    int winningScore;
    Enemy enemyPaddle;
    Ball ball;
    Player playerPaddle;
    AudioStreamPlayer2D soundtrack;
    AudioStreamPlayer2D playerScored;
    AudioStreamPlayer2D enemyScored;
    AudioStreamPlayer2D lostGame;
    AudioStreamPlayer2D wonGame;
    Properties propertiesValues;


    public override void _Ready()
    {
        propertiesValues = GetNode<Properties>("/root/Properties");
        winningScore = propertiesValues.winningScore;
        enemyPaddle = GetNode<Enemy>("Paddles/Enemy");
        playerPaddle = GetNode<Player>("Paddles/Player");
        ball = GetNode<Ball>("Ball");
        soundtrack = GetNode<AudioStreamPlayer2D>("Audio/SoundTrack");
        lostGame = GetNode<AudioStreamPlayer2D>("Audio/PlayerLost");
        wonGame = GetNode<AudioStreamPlayer2D>("Audio/PlayerWon");
        playerScored = GetNode<AudioStreamPlayer2D>("Audio/PlayerScored");
        enemyScored = GetNode<AudioStreamPlayer2D>("Audio/EnemyScored");
        soundtrack.Play();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("launch") && !ball.HasLaunched)
        {
            ball.Launch();
        }

        if (Input.IsActionPressed("ui_cancel"))
        {
            GetTree().ChangeSceneToFile("res://Table/Menu.tscn");
        }

        if (!ball.HasLaunched)
        {
            Vector2 inputValue = Vector2.Zero;
            inputValue.y = playerPaddle.Position.y + 50;
            inputValue.x = playerPaddle.Position.x + 18;
            ball.Position = inputValue;
        }
    }

    private void StartNewGame()
    {
        ball.Reset();
        enemyPaddle.Reset();
        playerPaddle.Reset();
        GetNode<Label>("Labels/PlayerScore").Text = "0";
        GetNode<Label>("Labels/EnemyScore").Text = "0";
        GetNode<Label>("Labels/GameOverLeft").Hide();
        GetNode<Label>("Labels/GameOverRight").Hide();
        gameHasEnded = false;
        soundtrack.Play();
    }

    public void BallEnteredPlayerGoal(Node2D body)
    {
        enemyScored.Play();
        UpdateScore("Labels/EnemyScore");
    }

    public void BallEnteredEnemyGoal(Node2D body)
    {
        playerScored.Play();
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
            soundtrack.Stop();
            Label gameOverRight = GetNode<Label>("Labels/GameOverRight");
            Label gameOverLeft = GetNode <Label>("Labels/GameOverLeft");
            if (scoreToUpdate == "Labels/PlayerScore")
            {
                wonGame.Play();
                gameOverLeft.Text = "Game Over\nYou won!!";
            }
            else
            {
                lostGame.Play();
                gameOverLeft.Text = "Game Over\nYou Lost...";
            }
            gameOverLeft.Show();
            gameOverRight.Show();
            gameHasEnded = true;
        }
        else
        {
            ball.Reset();
            enemyPaddle.Reset();
            playerPaddle.Reset();
        }
    }
}
