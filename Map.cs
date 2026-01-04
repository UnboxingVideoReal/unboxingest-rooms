using Godot;
using System;
using System.Collections.Generic;
using UnboxingestRooms.Rooms;

public partial class Map : Control
{
    public List<string> seededRooms = Main.seededRooms;
    public Vector2 windowSize;
    Vector2 startPos;

    public override void _Ready()
    {
        windowSize = GetTree().CurrentScene.GetViewport().GetVisibleRect().Size;
        startPos = new Vector2(windowSize.X / 2, windowSize.Y / 2);
        Bro();
    }
    public void Bro()
    {
        int wawa = 0;
        Vector2 savedPos = startPos;
        foreach (string room in seededRooms)
        {
            if (wawa != 0)
            {
                AnimatedSprite2D rooom = GetNode<AnimatedSprite2D>("room").Duplicate() as AnimatedSprite2D;
                rooom.Modulate = new Color(1, 1, 1, 1f);
                rooom.ZIndex = 1000;
                AddChild(rooom);
                string wawaw = room.Split(",")[4];
                if (wawaw == "l-") // 1,A,a,a,l-
                {
                    rooom.GlobalPosition = savedPos + new Vector2(-5, 0);
                }
                else if (wawaw == "r-")
                {
                    rooom.GlobalPosition = savedPos + new Vector2(5, 0);
                }
                else if (wawaw == "u-")
                {
                    rooom.GlobalPosition = savedPos + new Vector2(0, -5);
                }
                else if (wawaw == "d-")
                {
                    rooom.GlobalPosition = savedPos + new Vector2(0, 5);
                }
                savedPos = rooom.GlobalPosition;
            }
            else
            {
                AnimatedSprite2D rooom = GetNode<AnimatedSprite2D>("room").Duplicate() as AnimatedSprite2D;
                rooom.Modulate = new Color(1, 0, 0, 1f);
                rooom.ZIndex = 1001;
                AddChild(rooom);
                string wawaw = room.Split(",")[4];
                if (wawaw == "l-") // 1,A,a,a,l-
                {
                    rooom.GlobalPosition = savedPos + new Vector2(-5, 0);
                }
                else if (wawaw == "r-")
                {
                    rooom.GlobalPosition = savedPos + new Vector2(5, 0);
                }
                else if (wawaw == "u-")
                {
                    rooom.GlobalPosition = savedPos + new Vector2(0, -5);
                }
                else if (wawaw == "d-")
                {
                    rooom.GlobalPosition = savedPos + new Vector2(0, 5);
                }
                savedPos = rooom.GlobalPosition;
            }
            wawa++;
        }
    }
    public override void _Process(double delta)
    {

    }
}
