using Godot;
using System;

public partial class Menu : Control
{
	public void OnOptionsPressed()
	{
		GetTree().ChangeSceneToFile("res://Table/Options.tscn");
	}

	public void OnStartPressed()
	{
		GetTree().ChangeSceneToFile("res://Table/table.tscn");
	}

	public void OnQuitPressed()
	{
		GetTree().Quit();
	}

	public void OnLinkButtonPressed()
	{
		System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://github.com/joseph-behrens/jong") { UseShellExecute = true });
	}

	public void OnCreditsPressed()
	{
        GetTree().ChangeSceneToFile("res://Table/Credits.tscn");
    }
}
