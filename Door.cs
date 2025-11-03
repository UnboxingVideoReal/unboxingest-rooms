using Godot;
using System;
using System.Text.RegularExpressions;
using System.Transactions;
using UnboxingestRooms.Rooms;

public partial class Door : AnimatedSprite2D
{
    int[] frameDoor = [0, 1, 2, 4];
    int[] frameOnOpen = [2, 2, 3, 5];

    string[] possibleDirs = ["left", "right", "up", "down"];
    public static string dir;

    bool open = false;

    public static bool touching = false;

    public override void _Ready()
    {
        onRoom();
    }
    public void onRoom()
    {
        GD.Print("door");
        Main.currentRoom = GetTree().CurrentScene.GetNode<Node2D>("CurRoom").GetNode<Node2D>("Room");
        dir = possibleDirs[Main.rand.RandiRange(0, 3)];
        GetNode<AnimatedSprite2D>("UpDoor").Visible = false;
        switch (dir)
        {
            case "left":
                Position = new Vector2(92, 750);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[0];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;

                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 1;
                ZIndex = 4;

                break;
            case "right":
                Position = new Vector2(1827, 750);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[1];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 1;
                ZIndex = 4;

                break;
            case "up":
                Position = new Vector2(960, 460);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[2];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 1;
                ZIndex = 2;

                break;
            case "down":
                Position = new Vector2(960, 880);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[3];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 0;
                ZIndex = 4;

                break;
        }
    }
    public override void _Process(double delta)
    {
        Area2D area = GetNode<Area2D>("Area2D");
        if (area.HasOverlappingBodies())
        {
            touching = true;
        }
        else
        {
            touching = false;
        }
        if (Input.IsActionJustPressed("Interact") && area.GetOverlappingBodies().Count > 0 /*.Contains("")? idk*/)
        {
            open = true;
            Frame = frameOnOpen[Array.IndexOf(possibleDirs, dir)];
            GetNode<AudioStreamPlayer2D>("Sound").Play();
            if (dir == "up")
            {
                GetNode<AnimatedSprite2D>("UpDoor").Visible = true;
            }
        }
    }
}
