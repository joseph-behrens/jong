using Godot;
using System;

public partial class Properties : Node
{
	public Levels cpuPaddleSpeed { get; set; } = Levels.NORMAL;
	public Levels playerPaddleSpeed { get; set; } = Levels.NORMAL;
	public int winningScore { get; set; } = 11;
    public enum Levels { SLOW = 1000, NORMAL = 2000, FAST = 3000 }
}
