using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Transactions;
using UnboxingestRooms.Rooms;

public partial class Door : AnimatedSprite2D
{
    int[] frameDoor = [0, 1, 2, 4];
    int[] frameOnOpen = [2, 2, 3, 5];

    string[] possibleDirs = ["l-", "r-", "u-", "d-"];

    List<string> seededRooms = Main.seededRooms;
    public static string dir;

    public static bool open = false;

    public static bool touching = false;

    public override void _Ready()
    {
        onRoom();
    }
    public void onRoom()
    {
        GD.Print("door");
        open = false;
        Main.currentRoom = GetTree().CurrentScene.GetNode<Node2D>("CurRoom").GetNode<Node2D>("Room");
        dir = seededRooms[Main.room].Split(",")[4];
        GD.Print(dir);
        GetNode<AnimatedSprite2D>("UpDoor").Visible = false;
        AnimatedSprite2D label = GetNode<AnimatedSprite2D>("Label");
        label.GetNode<RichTextLabel>("RichTextLabel").Text = "[color=black]A-" + (Main.room + 1).ToString("D3");
        switch (dir)
        {
            case "l-":
                Position = new Vector2(92, 750);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[0];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                label.Visible = false;

                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 1;
                ZIndex = 4;

                break;
            case "r-":
                Position = new Vector2(1827, 750);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[1];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                label.Visible = false;

                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 1;
                ZIndex = 4;

                break;
            case "u-":
                Position = new Vector2(960, 460);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[2];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                label.Visible = true;
                label.Frame = 0;
                label.GetNode<RichTextLabel>("RichTextLabel").Position = label.Position + new Vector2(-54.0f, -139.0f);

                Main.currentRoom.GetNode<AnimatedSprite2D>("Wall2").Frame = 1;
                ZIndex = 2;

                break;
            case "d-":
                Position = new Vector2(960, 880);
                Scale = new Vector2(2, 2);
                Frame = frameDoor[3];
                GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>(dir + "Collision").Disabled = false;
                label.Visible = true;
                label.Frame = 1;
                label.GetNode<RichTextLabel>("RichTextLabel").Position = label.Position + new Vector2(-54.0f, 64.5f);

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
            if (dir == "u-")
            {
                GetNode<AnimatedSprite2D>("UpDoor").Visible = true;
            }
        }
    }
}
