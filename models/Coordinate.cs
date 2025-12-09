using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Coordinate
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Coordinate(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static double GetDistance(Coordinate a, Coordinate b)
    {
        return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2) + Math.Pow(b.Z - a.Z, 2));
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

}
