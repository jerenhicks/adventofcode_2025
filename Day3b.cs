using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day3b : Day
{
    private static string dataFilePath = @"data-files/day3/star2/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day3b";
    private double elapsedMilliseconds = 0.0;
    private List<NeoBatteryBank> batteryBanks = new List<NeoBatteryBank>();

    public Day3b()
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
        // ...existing code...
        foreach (var line in System.IO.File.ReadLines(filePath))
        {
            // Parse each character as an int and add to a list
            var intList = new List<int>();
            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    intList.Add((int)char.GetNumericValue(c));
                }
            }
            List<Battery> batteryList = intList.Select(capacity => new Battery { capacity = capacity }).ToList();
            batteryBanks.Add(new NeoBatteryBank(batteryList));
        }
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        foreach (var bank in batteryBanks)
        {
            bank.calculateLargestCapacity();
            sumofValues += bank.largestCapacity;
        }
        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "173577199527257";
    }
}