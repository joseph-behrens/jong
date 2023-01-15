using Godot;
using System;

public partial class Options : Control
{
	OptionButton cpuSpeedSelection;
	OptionButton playerSpeedSpeedSelection;
	LineEdit scoreToWinField;
	Properties propertiesValues;


	public override void _Ready()
	{

        string[] speedLevels = Enum.GetNames(typeof(Properties.Levels));

        propertiesValues = GetNode<Properties>("/root/Properties");
		cpuSpeedSelection = GetNode<OptionButton>("GridContainer/CpuSpeed");
		playerSpeedSpeedSelection = GetNode<OptionButton>("GridContainer/PlayerSpeed");
		scoreToWinField = GetNode<LineEdit>("GridContainer/ScoreToWin");

		foreach (var level in speedLevels)
		{ 
			cpuSpeedSelection.AddItem(level);
            playerSpeedSpeedSelection.AddItem(level);
        }

		scoreToWinField.Text = propertiesValues.winningScore.ToString();
        cpuSpeedSelection.Select(Array.IndexOf(speedLevels, propertiesValues.playerPaddleSpeed.ToString()));
        playerSpeedSpeedSelection.Select(Array.IndexOf(speedLevels, propertiesValues.playerPaddleSpeed.ToString()));
	}

	public void OnSavePressed()
	{
        propertiesValues.winningScore = Int32.Parse(GetNode<LineEdit>("GridContainer/ScoreToWin").Text);
		propertiesValues.playerPaddleSpeed = Enum.Parse<Properties.Levels>(playerSpeedSpeedSelection.Text);
        propertiesValues.cpuPaddleSpeed = Enum.Parse<Properties.Levels>(cpuSpeedSelection.Text); ;
        GoToMenuScene();
    }

	public void OnCancelPressed()
	{
		GoToMenuScene();
    }

	private void GoToMenuScene()
	{
        GetTree().ChangeSceneToFile("res://Table/Menu.tscn");
    }
}
