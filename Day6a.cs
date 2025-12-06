using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day6a : Day
{
    private static string dataFilePath = @"data-files/day6/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day6a";
    private double elapsedMilliseconds = 0.0;
    private List<Equation> equations = new List<Equation>();

    public Day6a()
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

        var firstTime = true;
        foreach (var line in System.IO.File.ReadLines(filePath))
        {

            var lineValues = line.Split(" ").ToList();
            //are there any more spaces cause of weird spacing?
            lineValues = lineValues.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            for (int i = 0; i < lineValues.Count; i++)
            {
                if (firstTime)
                {
                    equations.Add(new Equation());
                }
                var currentEquation = equations[i];
                if (lineValues[i] == "+")
                {
                    currentEquation.SetOperation(Operation.Add);
                    continue;
                }
                else if (lineValues[i] == "-")
                {
                    currentEquation.SetOperation(Operation.Subtract);
                    continue;
                }
                else if (lineValues[i] == "*")
                {
                    currentEquation.SetOperation(Operation.Multiply);
                    continue;
                }
                else if (lineValues[i] == "/")
                {
                    currentEquation.SetOperation(Operation.Divide);
                    continue;
                }
                else
                {
                    currentEquation.AddNumber(Double.Parse(lineValues[i]));
                }
            }
            firstTime = false;
        }

    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        sumofValues = 0;
        foreach (var eq in equations)
        {
            sumofValues += eq.Evaluate();
        }


        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "5227286044585";
    }
}