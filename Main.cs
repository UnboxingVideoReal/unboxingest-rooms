using Godot;
using System;
using static Godot.DisplayServer;

public partial class Main : Node2D
{
    public static int room = 0;
    public static RandomNumberGenerator rand = new RandomNumberGenerator();
    public static Node2D currentRoom;

    public static Node tree;
    public Window Window => GetWindow();

    public override void _Ready()
    {
        Window.Position = new Vector2I(0, 0);
    }
    public override void _Process(double delta)
    {
        currentRoom = GetTree().CurrentScene.GetNode<Node2D>("CurRoom").GetNode<Node2D>("Room");
        tree = GetTree().CurrentScene;
        if (Input.IsActionJustPressed("Fullscreen"))
        {
            Window.Mode = Window.Mode == Window.ModeEnum.Fullscreen ? Window.ModeEnum.Windowed : Window.ModeEnum.Fullscreen;
        }
    }

    public static void newRoom()
    {
        GD.Print("start");
        tree.GetNode<Player>("Player").onRoom();
        room += 1;
        currentRoom.GetNode<Door>("Door").onRoom();
    }
}
