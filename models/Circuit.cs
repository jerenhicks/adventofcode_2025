using System;
using System.Collections.Generic;

public class Circuit
{
    public List<(Coordinate, Coordinate)> Pairs { get; set; }
    public List<Coordinate> UniqueCoordinates { get; set; }

    public Circuit()
    {
        Pairs = new List<(Coordinate, Coordinate)>();
        UniqueCoordinates = new List<Coordinate>();
    }

    public void AddPair((Coordinate, Coordinate) pair)
    {
        Pairs.Add(pair);
        if (!UniqueCoordinates.Contains(pair.Item1))
        {
            UniqueCoordinates.Add(pair.Item1);
        }
        if (!UniqueCoordinates.Contains(pair.Item2))
        {
            UniqueCoordinates.Add(pair.Item2);
        }
    }

    public bool OverlapsWith((Coordinate, Coordinate) pair)
    {
        return UniqueCoordinates.Contains(pair.Item1) || UniqueCoordinates.Contains(pair.Item2);
    }

    public Boolean OverlapsWith(Circuit other)
    {
        foreach (var coord in other.UniqueCoordinates)
        {
            if (UniqueCoordinates.Contains(coord))
            {
                return true;
            }
        }
        return false;
    }

    public void Merge(Circuit other)
    {
        foreach (var pair in other.Pairs)
        {
            AddPair(pair);
        }
    }
}