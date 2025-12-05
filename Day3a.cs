using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day3a : Day
{
    private static string dataFilePath = @"data-files/day3/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day3a";
    private double elapsedMilliseconds = 0.0;
    private List<BatteryBank> batteryBanks = new List<BatteryBank>();

    public Day3a()
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
            batteryBanks.Add(new BatteryBank(intList));
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
        return "17535";
    }
}