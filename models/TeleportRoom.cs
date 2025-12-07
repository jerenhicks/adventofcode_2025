using System;
using System.Collections.Generic;

public class TeleportRoom
{
    private int maxWidth = 0;
    private int maxHeight = 0;
    private Dictionary<(int, int), TeleportFloorTile> tilesByCoordinates = new Dictionary<(int, int), TeleportFloorTile>();

    public void AddTile(int x, int y, TeleportFloorTileType tileType)
    {
        TeleportFloorTile tile = new TeleportFloorTile();
        tile.TileType = tileType;
        tilesByCoordinates[(x, y)] = tile;
        if (x > maxWidth) maxWidth = x;
        if (y > maxHeight) maxHeight = y;
    }

    public void ProcessBeam()
    {
        for (int y = 1; y <= maxHeight; y++)
        {
            for (int x = 0; x <= maxWidth; x++)
            {
                var coord = (x, y);
                TeleportFloorTile tileTypeAbove = GetFileAbove(x, y);
                TeleportFloorTile tileType = GetTileTypeAt(x, y);

                if (tileTypeAbove.TileType == TeleportFloorTileType.Manifold)
                {
                    tilesByCoordinates[coord].TileType = TeleportFloorTileType.Beam;
                    tilesByCoordinates[coord].Score = 1;
                }
                else if (tileTypeAbove.TileType == TeleportFloorTileType.Beam)
                {
                    if (tileType.TileType == TeleportFloorTileType.Splitter)
                    {
                        tilesByCoordinates[(x - 1, y)].TileType = TeleportFloorTileType.Beam;
                        tilesByCoordinates[(x - 1, y)].Score += tileTypeAbove.Score;
                        tilesByCoordinates[(x + 1, y)].TileType = TeleportFloorTileType.Beam;
                        tilesByCoordinates[(x + 1, y)].Score += tileTypeAbove.Score;
                    }
                    else
                    {
                        tilesByCoordinates[coord].TileType = TeleportFloorTileType.Beam;
                        tilesByCoordinates[coord].Score += tileTypeAbove.Score;
                    }
                }
            }
        }
    }

    public TeleportFloorTile GetTileTypeAt(int x, int y)
    {
        var coord = (x, y);
        if (tilesByCoordinates.ContainsKey(coord))
        {
            return tilesByCoordinates[coord];
        }
        throw new System.Exception($"No tile at coordinates {x},{y}");
    }

    public TeleportFloorTile GetFileAbove(int x, int y)
    {
        if (y <= 0) throw new System.Exception("No tile above the top row");
        return GetTileTypeAt(x, y - 1);
    }

    public int CountBeamSplits()
    {
        //iterate tileTypeByCoordinates
        int splitCount = 0;
        foreach (var kvp in tilesByCoordinates)
        {
            if (kvp.Value.TileType == TeleportFloorTileType.Splitter)
            {
                //we found a splitter. If left and right side have beam, we know it's a split
                var x = kvp.Key.Item1;
                var y = kvp.Key.Item2;
                // var leftTileType = GetTileTypeAt(x - 1, y);
                // var rightTileType = GetTileTypeAt(x + 1, y);
                //get tile above to check
                var aboveTileType = GetFileAbove(x, y);
                if (aboveTileType.TileType == TeleportFloorTileType.Beam)
                {
                    splitCount++;
                }
            }
        }
        return splitCount;
    }

    public void PrintRoom()
    {
        for (int y = 0; y <= maxHeight; y++)
        {
            for (int x = 0; x <= maxWidth; x++)
            {
                var coord = (x, y);
                if (tilesByCoordinates.ContainsKey(coord))
                {
                    var tileType = tilesByCoordinates[coord].TileType;
                    char c = ' ';
                    switch (tileType)
                    {
                        case TeleportFloorTileType.None:
                            c = '.';
                            break;
                        case TeleportFloorTileType.Manifold:
                            c = 'S';
                            break;
                        case TeleportFloorTileType.Splitter:
                            c = '^';
                            break;
                        case TeleportFloorTileType.Beam:
                            c = '|';
                            break;
                    }
                    System.Console.Write(c);
                }
                else
                {
                    System.Console.Write(' ');
                }
            }
            System.Console.WriteLine();
        }
    }

    public double ScoreLastRow()
    {
        var runningScore = 0.0;
        for (int x = 0; x <= maxWidth; x++)
        {
            var coord = (x, maxHeight);
            if (tilesByCoordinates.ContainsKey(coord))
            {
                var tile = tilesByCoordinates[coord];
                runningScore += tile.Score;
            }
        }
        return runningScore;
    }

    public void ScoreAllRows()
    {
        for (int y = 0; y <= maxHeight; y++)
        {
            var runningScore = 0.0;
            for (int x = 0; x <= maxWidth; x++)
            {
                var coord = (x, y);
                if (tilesByCoordinates.ContainsKey(coord))
                {
                    var tile = tilesByCoordinates[coord];
                    runningScore += tile.Score;
                }
            }
            System.Console.WriteLine($"Row {y} score: {runningScore}");
        }
    }
}