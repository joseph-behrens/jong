using Godot;
using System;

public partial class main : Node2D
{
    [Export]
    int winningScore = 11;
    Enemy enemyPaddle;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemyPaddle = GetNode<Enemy>("Enemy");
    }

    public void BallEnteredPlayerGoal(Node2D body)
    {
        UpdateScore("EnemyScore");
    }

    public void BallEnteredEnemyGoal(Node2D body)
    {
        UpdateScore("PlayerScore");
    }

    private void UpdateScore(string scoreToUpdate)
    {
        Ball ball = GetNodeOrNull<Ball>("Ball");
        Label scoreLabel = GetNodeOrNull<Label>(scoreToUpdate);
        int currentScore = Int32.Parse(scoreLabel.Text);
        scoreLabel.Text = (currentScore + 1).ToString();
        if (currentScore + 1 == winningScore)
        {
            Label gameOver = GetNode<Label>("GameOver");
            if (scoreToUpdate == "PlayerScore")
            {
                gameOver.Text = "Game Over\nYou won!!";
            }
            else
            {
                gameOver.Text = "Game Over\nYou Lost...";
            }
            gameOver.Show();
        }
        else
        {
            ball.Reset();
            enemyPaddle.Reset();
        }
    }
}
