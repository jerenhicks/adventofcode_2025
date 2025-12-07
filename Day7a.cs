using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day7a : Day
{
    private static string dataFilePath = @"data-files/day7/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day7a";
    private double elapsedMilliseconds = 0.0;
    private TeleportRoom teleportRoom = new TeleportRoom();

    public Day7a()
    {

    }

    public override double GetElapsedTime()
    {
        return elapsedMilliseconds;
    }

    public override string GetIdentifier()
    {
        return DayIdentifier;
    }

    public override string GetResult()
    {
        return $"{sumofValues}";
    }

    private void ReadValues(string filePath)
    {

        var lines = System.IO.File.ReadLines(filePath).ToList();
        int y = 0;
        foreach (var line in lines)
        {
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                TeleportFloorTileType tileType = TeleportFloorTileType.None;
                if (c == 'S')
                {
                    tileType = TeleportFloorTileType.Manifold;
                }
                else if (c == '^')
                {
                    tileType = TeleportFloorTileType.Splitter;
                }
                teleportRoom.AddTile(x, y, tileType);
            }
            y++;
        }
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        teleportRoom.ProcessBeam();
        //teleportRoom.PrintRoom();
        sumofValues = teleportRoom.CountBeamSplits();


        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "1615";
    }
}