using Godot;
using System;

public partial class Credits : Control
{
    public void OnKenneyLinkPressed()
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://www.kenney.nl/assets?q=audio") { UseShellExecute = true });
    }

    public void On99SoundsLinkPressed()
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://99sounds.org/project-pegasus") { UseShellExecute = true });
    }
}
