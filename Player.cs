using Godot;
using System;
using UnboxingestRooms.Abstract;

public partial class Player : CharacterBody2D
{
    private float Speed = 300f;
    private Vector2 velocity = Vector2.Zero;
    public bool walking = false;
    public string floorsound = "Carpet";
    private int wawa = 0;
    string[] possibleDirs = ["left", "right", "up", "down"];
    bool opened = false;


    public override void _Process(double delta)
    {
        Area2D plane = Main.currentRoom.GetNode<StaticBody2D>("StaticBody2D2").GetNode<Area2D>("Area2D");
        if (Door.touching && plane.HasOverlappingBodies() && Door.open && !opened)
        {
            Main.newRoom();
            opened = true;
            GetTree().CreateTimer(0.2).Timeout += () =>
            {
                opened = false;
            };
        }
    }

    public void onRoom()
    {
        GD.Print("player");
        if (Door.dir == "r-")
        {
            Position = new Vector2(175, 825);
        }
        else if (Door.dir == "l-")
        {
            Position = new Vector2(1745, 825);
        }
        else if (Door.dir == "u-")
        {
            Position = new Vector2(960, 950);
        }
        else if (Door.dir == "d-")
        {
            Position = new Vector2(960, 600);
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        // chatgpt do the code for me
        velocity = Velocity;

        if (Input.IsActionPressed("Sprint"))
        {
            Speed = 600f;
        }
        else
        {
            Speed = 300f;
        }

        Vector2 dir = Input.GetVector("Left", "Right", "Up", "Down");
        if (dir != Vector2.Zero) // this?
        {
            velocity = dir * Speed;
            walking = true;
        }
        else
        {
            walking = false;
            velocity = velocity.Lerp(Vector2.Zero /* xero drop remember that guy */, 0.2f);
        } // oops
        // im keeping all of these comments btw
        if (walking)
        {
            // i forgot how to get nodes
            wawa += 1;
            if (wawa % (int)(9000 / Speed) == 0) // hi % i think daluyan taought me what that was
            {
                GetNode<AudioStreamPlayer2D>(floorsound).Play();
            }

        }

        ZIndex = 3;
        Velocity = velocity;
        MoveAndSlide();

    }
}
