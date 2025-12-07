using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Godot.DisplayServer;

public partial class Main : Node2D
{
    public static int room = 0;
    public static RandomNumberGenerator rand = new RandomNumberGenerator();
    public static Node2D currentRoom;

    public static Node tree;
    public Window Window => GetWindow();

    public static RandomNumberGenerator seed = new RandomNumberGenerator();

    public static List<string> seededRooms = new List<string>();

    public static string[] sections =
    {
        "A"
    };

    public static string[] subSections =
    {
        "a",
        "ae",
        "ai",
        "au",
        "ao",
        "aa"
    };
    public static int[] sSectionPoint =
    {
        0,
        100,
        100,
        300,
        600,
        600
    };

    public static string[] roomTypes =
    {
        "a"
    };

    public static string[] directions =
    {
        "l",
        "r",
        "d",
        "u"
    };


    public override void _Ready()
    {
        seed.Randomize();
        GD.Print(seed);
        seededRooms = RandomMap(1000, seed);
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

    public static List<string> RandomMap(int sectionSize, RandomNumberGenerator seed)
    {
        List<string> world = new List<string>();
        // main sections
        for (int y = 0; y < sections.Length;  y++)
        {
            for (int i = 0; i < sectionSize; i++)
            {
                world.Add(i.ToString() + "," + sections[seed.RandiRange(0, sections.Length - 1)] + "," + "a"/*subSections[seed.RandiRange(0, subSections.Length - 1)]*/ + "," + roomTypes[seed.RandiRange(0, roomTypes.Length - 1)] + "," + directions[seed.RandiRange(0, directions.Length - 1)] + "-");
            }
        }

        // remove the last -
        var str = new string(world[world.Count - 1]);
        str = str.Remove(str.Length - 1, 1);
        world.RemoveAt(world.Count - 1);
        world.Add(str);

        // list subsections
        for (int y = 0; y < subSections.Length; y++)
        {
            if (sections.Contains(subSections[y], StringComparer.OrdinalIgnoreCase))
            {

            }
            else
            {
                int sSectionSize = 300 + seed.RandiRange(-200, 50);
                int rand = seed.RandiRange(-10, 5);
                for (int i = sSectionPoint[y] + rand; i <= sSectionSize; i++)
                {
                    if (i >= 0 && i < world.Count)
                    {
                        world.Insert(i, i.ToString() + "," + sections[seed.RandiRange(0, sections.Length - 1)] + "," + subSections[y/*seed.RandiRange(0, subSections.Length - 1)*/] + "," + roomTypes[seed.RandiRange(0, roomTypes.Length - 1)] + "," + directions[seed.RandiRange(0, directions.Length - 1)] + "-");
                    }
                }

            }
        }

        GD.Print(string.Join("", world));
        return world;
    }
}
