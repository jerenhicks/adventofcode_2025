using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day5b : Day
{
    private static string dataFilePath = @"data-files/day5/star2/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day5b";
    private double elapsedMilliseconds = 0.0;
    private List<InventoryRange> inventoryRanges = new List<InventoryRange>();
    private List<double> inventory = new List<double>();

    public Day5b()
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

        // List<InventoryRange> mergedRanges = new List<InventoryRange>();

        bool mergingHappened;
        List<InventoryRange> workingCopy = new List<InventoryRange>();
        workingCopy.AddRange(inventoryRanges);
        do
        {
            List<InventoryRange> mergedRanges = new List<InventoryRange>();
            mergingHappened = false;
            foreach (var range in workingCopy)
            {
                bool merged = false;
                for (int i = 0; i < mergedRanges.Count; i++)
                {
                    var mergedRange = mergedRanges[i];
                    if (range.Overlaps(mergedRange) || range.Adjacent(mergedRange))
                    {
                        mergedRanges[i] = mergedRange.Merge(range);
                        merged = true;
                        mergingHappened = true;
                        break;
                    }
                }
                if (!merged)
                {
                    mergedRanges.Add(range);
                }
            }
            workingCopy = mergedRanges;
        } while (mergingHappened);


        // foreach (var range in inventoryRanges)
        // {
        //     bool merged = false;
        //     for (int i = 0; i < mergedRanges.Count; i++)
        //     {
        //         var mergedRange = mergedRanges[i];
        //         if (range.Overlaps(mergedRange) || range.Adjacent(mergedRange))
        //         {
        //             mergedRanges[i] = mergedRange.Merge(range);
        //             merged = true;
        //             break;
        //         }
        //     }
        //     if (!merged)
        //     {
        //         mergedRanges.Add(range);
        //     }
        // }

        List<double> availableInventory = new List<double>();
        double rangesCount = 0;
        foreach (var range in workingCopy)
        {
            //availableInventory.AddRange(range.GetRangeSize());
            rangesCount += range.GetRangeSizeCount();
        }

        // availableInventory = availableInventory.Distinct().ToList();
        // availableInventory.Sort();
        // sumofValues = availableInventory.Count();
        sumofValues = rangesCount;

        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "359913027576322";
    }
}