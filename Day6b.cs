using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Day6b : Day
{
    private static string dataFilePath = @"data-files/day6/star2/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day6b";
    private double elapsedMilliseconds = 0.0;
    private List<SquidEquation> equations = new List<SquidEquation>();

    public Day6b()
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
        var lines = System.IO.File.ReadLines(filePath).ToList();
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (firstTime)
            {
                var splitStrings = lines[i].Split(new char[] { '+', '*' }).ToList();

                var operations = lines[i].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                //remove any values of just ""
                splitStrings = splitStrings.Where(x => x != "").ToList();
                for (int j = 0; j < splitStrings.Count; j++)
                {
                    SquidEquation eq = new SquidEquation();
                    eq.SetOperation(operations[j] == "+" ? Operation.Add : Operation.Multiply);
                    if (j < splitStrings.Count - 1)
                    {
                        eq.SetSignificantDigits(splitStrings[j].Length);
                    }
                    else
                    {
                        eq.SetSignificantDigits(splitStrings[j].Length + 1);
                    }
                    //unless it's the last one, then we add one to the length

                    equations.Add(eq);
                }
                firstTime = false;
            }
            else
            {
                int currentIndex = 0;
                foreach (SquidEquation eq in equations)
                {
                    List<double> numbers = new List<double>();
                    for (int q = currentIndex; q <= currentIndex + eq.significantDigits - 1; q++)
                    {
                        char c = lines[i][q];
                        if (char.IsDigit(c))
                        {
                            numbers.Add(double.Parse(c.ToString()));
                        }
                        else
                        {
                            numbers.Add(-1);//non-digit placeholder

                        }
                    }
                    eq.AddNumber(numbers);
                    currentIndex = currentIndex + eq.significantDigits + 1;
                }

            }
        }

    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        sumofValues = 0;
        foreach (var eq in equations)
        {
            sumofValues += eq.SquidMathEvaluate();
        }


        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "10227753257799";
    }
}