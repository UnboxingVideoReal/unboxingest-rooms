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
    void FixControl(TextureRect c)
    {
        c.AnchorLeft = 0;
        c.AnchorTop = 0;
        c.AnchorRight = 0;
        c.AnchorBottom = 0;
    }

    public void Bro()
    {
        int wawa = 0;
        Vector2 savedPos = new Vector2(0, 0);
        foreach (string room in seededRooms)
        {

            if (wawa != 0)
            {
                TextureRect rooom = GetNode<TextureRect>("room").Duplicate() as TextureRect;
                AddChild(rooom);
                FixControl(rooom);
                string wawaw = room.Split(",")[4];
                if (wawaw == "l-") // 1,A,a,a,l-
                {
                    rooom.Position = savedPos + new Vector2(5 + wawa, 0);
                }
                else if (wawaw == "r-")
                {
                    rooom.Position = savedPos + new Vector2(5 - wawa, 0);
                }
                else if (wawaw == "u-")
                {
                    rooom.Position = savedPos + new Vector2(wawa, 5 + wawa);
                }
                else if (wawaw == "d-")
                {
                    rooom.Position = savedPos + new Vector2(wawa, 5 - wawa);
                }
                savedPos = rooom.Position;
            }
            else
            {
                GetNode<TextureRect>("room").Position = startPos;
                FixControl(GetNode<TextureRect>("room"));
                savedPos = GetNode<TextureRect>("room").Position;
            }
            wawa++;
        }
    }
    public override void _Process(double delta)
    {

    }
}
