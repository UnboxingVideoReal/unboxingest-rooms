using Godot;
using System;

public partial class UI : Control
{
    public RichTextLabel RoomText => GetNode<RichTextLabel>("RichTextLabel");

    public override void _Process(double delta)
    {
        string wawa = Main.room.ToString("D3");
        RoomText.Text = "Room: A-" + wawa;
    }
}
