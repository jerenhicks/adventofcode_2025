using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Dynamic;

public class Day8a : Day
{
    private static string dataFilePath = @"data-files/day8/star1/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day8a";
    private double elapsedMilliseconds = 0.0;
    List<Coordinate> junctionBoxes = new List<Coordinate>();

    public Day8a()
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

        Coordinate cord1 = new Coordinate(162, 817, 812);
        Coordinate cord2 = new Coordinate(425, 690, 689);
        //Console.WriteLine($"Distance between cord1 and cord2: {Coordinate.GetDistance(cord1, cord2)}");

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
        var test = coordinatesScores.OrderByDescending(kvp => kvp.Key).ToList();

        var amountToPick = 1000;
        var topValues = coordinatesScores.OrderBy(kvp => kvp.Key)
                        .Take(amountToPick)
                        .Select(kvp => kvp.Value)
                        .ToList();

        List<List<(Coordinate, Coordinate)>> lumpedIntoCircuits = new List<List<(Coordinate, Coordinate)>>();

        foreach (var pair in topValues)
        {
            var foundCircuit = false;
            foreach (var circuit in lumpedIntoCircuits)
            {
                var overlaps = circuit.Where(c =>
                    c.Item1.Equals(pair.Item1) || c.Item1.Equals(pair.Item2) ||
                    c.Item2.Equals(pair.Item1) || c.Item2.Equals(pair.Item2)
                ).ToList();

                if (overlaps.Count > 0)
                {
                    circuit.Add(pair);
                    foundCircuit = true;
                    break;
                }
            }
            if (!foundCircuit)
            {
                lumpedIntoCircuits.Add(new List<(Coordinate, Coordinate)> { pair });
            }
        }

        //go through the lumpedIntoCircuits and see if we can merge any circuits that share coordinates
        bool mergingHappened;
        do
        {
            mergingHappened = false;
            for (int i = 0; i < lumpedIntoCircuits.Count; i++)
            {
                for (int j = i + 1; j < lumpedIntoCircuits.Count; j++)
                {
                    var circuitA = lumpedIntoCircuits[i];
                    var circuitB = lumpedIntoCircuits[j];

                    var overlaps = circuitA.Where(pairA =>
                        circuitB.Any(pairB =>
                            pairA.Item1.Equals(pairB.Item1) || pairA.Item1.Equals(pairB.Item2) ||
                            pairA.Item2.Equals(pairB.Item1) || pairA.Item2.Equals(pairB.Item2)
                        )
                    ).ToList();

                    if (overlaps.Count > 0)
                    {
                        //merge circuitB into circuitA
                        circuitA.AddRange(circuitB);
                        lumpedIntoCircuits.RemoveAt(j);
                        mergingHappened = true;
                        break;
                    }
                }
                if (mergingHappened)
                {
                    break;
                }
            }
        } while (mergingHappened);

        //go through and print out the circuits, only showing the unique values on each
        sumofValues = 1;
        List<double> scores = new List<double>();
        foreach (var circuit in lumpedIntoCircuits)
        {
            var uniqueCoordinates = new HashSet<Coordinate>();
            foreach (var pair in circuit)
            {
                uniqueCoordinates.Add(pair.Item1);
                uniqueCoordinates.Add(pair.Item2);
            }
            // Console.WriteLine("Circuit:");
            // foreach (var coord in uniqueCoordinates)
            // {
            //     Console.WriteLine($"  Coordinate: ({coord.X}, {coord.Y}, {coord.Z})");
            // }
            scores.Add(uniqueCoordinates.Count);
        }

        var topScores = scores.OrderByDescending(s => s).Take(3);
        foreach (var score in topScores)
        {
            sumofValues *= score;
        }

        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public override string Checksum()
    {
        return "75680";
    }
}