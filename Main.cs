using Godot;
using System;

public partial class Main : Node2D
{
    public static int room = 0;
    public static RandomNumberGenerator rand = new RandomNumberGenerator();
    public static Node2D currentRoom;

    public static Node tree;

    public override void _Process(double delta)
    {
        currentRoom = GetTree().CurrentScene.GetNode<Node2D>("CurRoom").GetNode<Node2D>("Room");
        tree = GetTree().CurrentScene;
    }

    public static void newRoom()
    {
        GD.Print("start");
        tree.GetNode<Player>("Player").onRoom();
        room += 1;
        currentRoom.GetNode<Door>("Door").onRoom();
    }
}
