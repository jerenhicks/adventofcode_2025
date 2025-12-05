using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Day2 : Day
{
    private static string dataFilePath = @"data-files/day2/star1/data.txt";
    private double sumOfInvalidIDs = 0.0;
    private static string DayIdentifier = "Day2";
    private double elapsedMilliseconds = 0.0;

    public Day2()
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
        return $"{sumOfInvalidIDs}";
    }

    public List<ProductID> ReadValues(string filePath)
    {
        var list = new List<ProductID>();
        foreach (var line in System.IO.File.ReadLines(filePath))
        {
            foreach (var part in line.Split(','))
            {
                var ids = part.Split('-');
                if (ids.Length != 2) continue;
                if (double.TryParse(ids[0], out double firstID) && double.TryParse(ids[1], out double secondID))
                {
                    list.Add(new ProductID { FirstID = firstID, SecondID = secondID });
                }
            }
        }
        return list;
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();
        List<ProductID> productIDs = ReadValues(dataFilePath);
        foreach (var pid in productIDs)
        {
            for (double i = pid.FirstID; i <= pid.SecondID; i++)
            {
                //easiest check would be to see if the number is odd number of digits
                var numberAsString = i.ToString();
                var numberAsStringLength = numberAsString.Length;
                if (numberAsStringLength % 2 == 0)
                {
                    //split the number in half and compare digits
                    var firstHalf = numberAsString.Substring(0, numberAsStringLength / 2);
                    var secondHalf = numberAsString.Substring(numberAsStringLength / 2);
                    if (firstHalf == secondHalf)
                    {
                        sumOfInvalidIDs += i;
                    }

                }
            }
        }
        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
        //Console.WriteLine($"Sum of Invalid Product IDs: {sumOfInvalidIDs}");
    }

    public override string Checksum()
    {
        return "18893502033";
    }
}