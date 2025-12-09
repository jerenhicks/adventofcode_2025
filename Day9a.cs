using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;

public class Day9a : Day
{
    private static string dataFilePath = @"data-files/day9/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day9a";
    private double elapsedMilliseconds = 0.0;
    List<TileCoordinate> tiles = new List<TileCoordinate>();

    public Day9a()
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
        foreach (var line in lines)
        {
            var items = line.Split(',').ToList();
            // Parse each item in the string array to int
            var intItems = items.Select(s => int.Parse(s)).ToList();
            TileCoordinate coord = new TileCoordinate(intItems[0], intItems[1]);
            tiles.Add(coord);
        }
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);
        for (int x = 0; x < tiles.Count; x++)
        {
            for (int y = 0; y < tiles.Count; y++)
            {
                //check area of this rectangle
                double height = Math.Abs(tiles[y].Y - tiles[x].Y) + 1;
                double width = Math.Abs(tiles[y].X - tiles[x].X) + 1;

                double area = height * width;
                if (area > sumofValues)
                {
                    sumofValues = area;
                }
            }
        }


        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "4777967538";
    }
}