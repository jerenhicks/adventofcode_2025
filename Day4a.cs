using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day4a : Day
{
    private static string dataFilePath = @"data-files/day4/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day4a";
    private double elapsedMilliseconds = 0.0;
    private FloorItemContainer floorItemContainer = null;

    public Day4a()
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
        return $"Sum of Values: {sumofValues}";
    }

    private void ReadValues(string filePath)
    {
        List<FloorItem> floorItems = new List<FloorItem>();
        int y = 0;
        foreach (var line in System.IO.File.ReadLines(filePath))
        {
            int x = 0;
            // Parse each character as an int and add to a list
            var intList = new List<int>();
            foreach (char c in line)
            {
                FloorItem item = new FloorItem();
                item.x = x;
                item.y = y;
                if (c == '@')
                {
                    item.isPaperRoll = true;
                }
                else
                {
                    item.isPaperRoll = false;
                }
                floorItems.Add(item);
                x++;
            }
            y++;
        }
        floorItemContainer = new FloorItemContainer(floorItems);
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);
        sumofValues = 0;

        foreach (var item in floorItemContainer.floorItems)
        {
            if (item.isPaperRoll)
            {
                var amount = floorItemContainer.HowManySurroundingPaperRolls(item.y, item.x);
                if (amount < 4)
                {
                    sumofValues++;
                }
            }
        }

        long endTime = Stopwatch.GetTimestamp();

        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }
}