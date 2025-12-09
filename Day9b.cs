using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;

public class Day9b : Day
{
    private static string dataFilePath = @"data-files/day9/star2/data.txt";
    private double sumofValues = 0.0;
    private static string DayIdentifier = "Day9b";
    private double elapsedMilliseconds = 0.0;
    List<TileCoordinate> tiles = new List<TileCoordinate>();
    Dictionary<(int, int), CarpetTile> tileMap = new Dictionary<(int, int), CarpetTile>();
    int maxHeight = 9;
    int maxWidth = 14;

    public Day9b()
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
            TileCoordinate coord = new TileCoordinate(intItems[1], intItems[0]);
            if (coord.X > maxWidth)
            {
                maxWidth = coord.X;
            }
            if (coord.Y > maxHeight)
            {
                maxHeight = coord.Y;
            }
            tiles.Add(coord);
        }
    }

    public override void Execute()
    {
        long startTime = Stopwatch.GetTimestamp();


        ReadValues(dataFilePath);

        TileCoordinate previousTile = null;
        List<TileCoordinate> polygonShapes = tiles;
        polygonShapes.Add(tiles[0]);
        tileMap[(polygonShapes[0].X, polygonShapes[0].Y)] = new CarpetTile(TileType.RED, true);
        previousTile = polygonShapes[0];
        for (int i = 1; i < polygonShapes.Count; i++)
        {
            TileCoordinate tileToAdd = polygonShapes[i];
            if (previousTile.X == tileToAdd.X)
            {
                int initialYDiff = previousTile.Y - tileToAdd.Y;
                int yDiff = Math.Abs(previousTile.Y - tileToAdd.Y) - 1;
                for (int j = yDiff; j > 0; j--)
                {
                    if (initialYDiff > 0)
                        tileMap[(tileToAdd.X, previousTile.Y - j)] = new CarpetTile(TileType.GREEN, true);
                    else
                        tileMap[(tileToAdd.X, previousTile.Y + j)] = new CarpetTile(TileType.GREEN, true);
                }
            }
            else
            {
                int initialXDiff = previousTile.X - tileToAdd.X;
                int xDiff = Math.Abs(previousTile.X - tileToAdd.X) - 1;
                for (int j = xDiff; j > 0; j--)
                {
                    if (initialXDiff > 0)
                        tileMap[(previousTile.X - j, tileToAdd.Y)] = new CarpetTile(TileType.GREEN, true);
                    else
                        tileMap[(previousTile.X + j, tileToAdd.Y)] = new CarpetTile(TileType.GREEN, true);
                }
            }

            tileMap[(tileToAdd.X, tileToAdd.Y)] = new CarpetTile(TileType.RED, true);
            previousTile = tileToAdd;
        }

        Console.WriteLine("Raytracing the map");
        RaytraceAndPaint();
        Console.WriteLine("Finished raytracing.");
        //PrintTileMap();

        long endTime = Stopwatch.GetTimestamp();
        elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;
    }

    public void RaytraceAndPaint()
    {
        int x, y;
        bool paintMode = false;
        bool pauseMode = false;

        for (y = 0; y <= maxHeight; y++)
        {
            paintMode = false;
            pauseMode = false;
            for (x = 0; x <= maxWidth; x++)
            {
                //probably not best, but if the tile doesn't exist at x,y, add it as none
                if (!tileMap.ContainsKey((x, y)))
                {
                    tileMap[(x, y)] = new CarpetTile(TileType.NONE, false);
                }
                if (paintMode && !pauseMode)
                {
                    if (tileMap[(x, y)].IsEdge)
                    {
                        pauseMode = true;
                        paintMode = false;
                    }
                    else
                    {
                        tileMap[(x, y)] = new CarpetTile(TileType.GREEN, false);
                    }
                }
                else
                {
                    if (tileMap[(x, y)].IsEdge)
                    {
                        if (tileMap[(x, y)].Type == TileType.RED)
                        {
                            pauseMode = true;

                        }
                        else
                        {
                            paintMode = true;
                        }
                    }
                }
            }
        }

        for (x = 0; x <= maxWidth; x++)
        {
            paintMode = false;
            pauseMode = false;
            for (y = 0; y <= maxHeight; y++)
            {
                //probably not best, but if the tile doesn't exist at x,y, add it as none
                if (!tileMap.ContainsKey((x, y)))
                {
                    tileMap[(x, y)] = new CarpetTile(TileType.NONE, false);
                }
                if (paintMode && !pauseMode)
                {
                    if (tileMap[(x, y)].IsEdge)
                    {
                        pauseMode = true;
                        paintMode = false;
                    }
                    else
                    {
                        tileMap[(x, y)] = new CarpetTile(TileType.GREEN, false);
                    }
                }
                else
                {
                    if (tileMap[(x, y)].IsEdge)
                    {
                        if (tileMap[(x, y)].Type == TileType.RED)
                        {
                            pauseMode = true;

                        }
                        else
                        {
                            paintMode = true;
                        }
                    }
                }
            }

        }

    }

    public void PrintTileMap()
    {

        for (int y = 0; y <= maxHeight; y++)
        {
            for (int x = 0; x <= maxWidth; x++)
            {
                if (tileMap.TryGetValue((x, y), out CarpetTile tile))
                {
                    switch (tile.Type)
                    {
                        case TileType.RED:
                            Console.Write("#");
                            break;
                        case TileType.GREEN:
                            Console.Write("X");
                            break;
                        default:
                            Console.Write(".");
                            break;
                    }
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public override string Checksum()
    {
        return "8995844880";
    }
}