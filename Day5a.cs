using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day5a : Day
{
    private static string dataFilePath = @"data-files/day5/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day5a";
    private double elapsedMilliseconds = 0.0;
    private List<InventoryRange> inventoryRanges = new List<InventoryRange>();
    private List<double> inventory = new List<double>();

    public Day5a()
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

        var inventoryRead = true;
        foreach (var line in System.IO.File.ReadLines(filePath))
        {
            if (line.Trim() == "")
            {
                inventoryRead = false;
                continue;
            }
            if (inventoryRead)
            {

                InventoryRange range = new InventoryRange(Double.Parse(line.Split('-')[0]), Double.Parse(line.Split('-')[1]));
                inventoryRanges.Add(range);
            }
            else
            {
                inventory.Add(double.Parse(line));
            }
            // Parse each character as an int and add to a list

        }

    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        foreach (var item in inventory)
        {
            foreach (var range in inventoryRanges)
            {
                if (range.IsInRange(item))
                {
                    sumofValues++;
                    break;
                }
            }
        }


        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "840";
    }
}