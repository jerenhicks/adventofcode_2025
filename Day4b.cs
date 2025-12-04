using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day4b : Day
{
    private static string dataFilePath = @"data-files/day4/star2/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day4b";
    private double elapsedMilliseconds = 0.0;
    private FloorItemContainer floorItemContainer = null;

    public Day4b()
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
        List<FloorItem> floorsToRemove = new List<FloorItem>();
        do
        {
            floorsToRemove.Clear();
            foreach (var item in floorItemContainer.floorItems)
            {
                if (item.isPaperRoll)
                {
                    var amount = floorItemContainer.HowManySurroundingPaperRolls(item.y, item.x);
                    if (amount < 4)
                    {
                        sumofValues++;
                        floorsToRemove.Add(item);
                    }
                }
            }
            foreach (var item in floorsToRemove)
            {
                floorItemContainer.RemovePaperRollAt(item.y, item.x);
            }
        } while (floorsToRemove.Count > 0);

        long endTime = Stopwatch.GetTimestamp();

        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }
}