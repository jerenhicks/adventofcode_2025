using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Dynamic;

public class Day8b : Day
{
    private static string dataFilePath = @"data-files/day8/star2/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day8b";
    private double elapsedMilliseconds = 0.0;
    List<Coordinate> junctionBoxes = new List<Coordinate>();

    public Day8b()
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
            Coordinate coord = new Coordinate(intItems[0], intItems[1], intItems[2]);
            junctionBoxes.Add(coord);
        }
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        Dictionary<double, (Coordinate, Coordinate)> coordinatesScores = new Dictionary<double, (Coordinate, Coordinate)>();

        for (int i = 0; i < junctionBoxes.Count; i++)
        {
            for (int j = i + 1; j < junctionBoxes.Count; j++)
            {
                var score = Coordinate.GetDistance(junctionBoxes[i], junctionBoxes[j]);
                //don't want to add any duplicates, check if the score is there with same coordinates
                if (coordinatesScores.ContainsKey(score))
                {
                    var existingPair = coordinatesScores[score];
                    if ((existingPair.Item1.Equals(junctionBoxes[i]) && existingPair.Item2.Equals(junctionBoxes[j])) ||
                        (existingPair.Item1.Equals(junctionBoxes[j]) && existingPair.Item2.Equals(junctionBoxes[i])))
                    {
                        continue; //skip adding duplicate
                    }
                }
                coordinatesScores[score] = (junctionBoxes[i], junctionBoxes[j]);
            }
        }
        //sort based on descending score
        //var test = coordinatesScores.OrderByDescending(kvp => kvp.Key).ToList();

        var amountToPick = 1000;
        var topValues = coordinatesScores.OrderBy(kvp => kvp.Key)
                        .Select(kvp => kvp.Value)
                        .Take(amountToPick)
                        .ToList();

        List<Circuit> lumpedIntoCircuits = new List<Circuit>();

        var foundCircuit = false;
        while (topValues.Count > 0)
        {
            Circuit circuit1 = new Circuit();
            circuit1.AddPair(topValues[0]);
            lumpedIntoCircuits.Add(circuit1);
            topValues.RemoveAt(0);

            var continueLumping = false;
            do
            {
                continueLumping = false;
                foreach (var pair in topValues.ToList())
                {
                    if (circuit1.OverlapsWith(pair))
                    {
                        circuit1.AddPair(pair);
                        if (circuit1.UniqueCoordinates.Count == junctionBoxes.Count)
                        {
                            //all junction boxes are connected, we can stop lumping
                            sumofValues = pair.Item1.X * pair.Item2.X;
                            continueLumping = false;
                            foundCircuit = true;
                            break;
                        }
                        topValues.Remove(pair);
                        continueLumping = true;
                    }
                }
            } while (continueLumping);
            if (foundCircuit)
            {
                break;
            }
        }


        // foreach (var pair in topValues)
        // {
        //     var foundCircuit = false;
        //     foreach (var circuit in lumpedIntoCircuits)
        //     {

        //         if (circuit.OverlapsWith(pair))
        //         {
        //             circuit.AddPair(pair);
        //             foundCircuit = true;
        //             break;
        //         }
        //     }
        //     if (!foundCircuit)
        //     {
        //         Circuit newCircuit = new Circuit();
        //         newCircuit.AddPair(pair);
        //         lumpedIntoCircuits.Add(newCircuit);
        //     }
        // }

        //go through the lumpedIntoCircuits and see if we can merge any circuits that share coordinates
        // Coordinate cord1 = null;
        // Coordinate cord2 = null;
        // bool mergingHappened;
        // do
        // {
        //     mergingHappened = false;
        //     for (int i = 0; i < lumpedIntoCircuits.Count; i++)
        //     {
        //         for (int j = i + 1; j < lumpedIntoCircuits.Count; j++)
        //         {
        //             var circuitA = lumpedIntoCircuits[i];
        //             var circuitB = lumpedIntoCircuits[j];

        //             if (circuitA.OverlapsWith(circuitB))
        //             {
        //                 //merge circuitB into circuitA
        //                 circuitA.Merge(circuitB);
        //                 lumpedIntoCircuits.RemoveAt(j);
        //                 if (circuitA.UniqueCoordinates.Count == junctionBoxes.Count)
        //                 {
        //                     //all junction boxes are connected, we can stop merging
        //                     cord1 = circuitA.UniqueCoordinates[circuitA.UniqueCoordinates.Count - 1];
        //                     cord2 = circuitA.UniqueCoordinates[circuitA.UniqueCoordinates.Count - 2];
        //                     mergingHappened = false;
        //                     break;
        //                 }
        //                 mergingHappened = true;
        //                 break;
        //             }
        //         }
        //         if (mergingHappened)
        //         {
        //             break;
        //         }
        //     }
        // } while (mergingHappened);

        //go through and print out the circuits, only showing the unique values on each
        // sumofValues = 1;
        // var topScores = lumpedIntoCircuits.Select(circuit => circuit.UniqueCoordinates.Count)
        //                 .OrderByDescending(s => s).Take(3);
        // foreach (var score in topScores)
        // {
        //     sumofValues *= score;
        // }

        //Console.WriteLine($"Last two connected coordinates: {cord1.ToString()} and {cord2.ToString()}");

        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "75680";
    }
}