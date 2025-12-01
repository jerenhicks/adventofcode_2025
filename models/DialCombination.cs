using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum DialDirection
{
    Left,
    Right
}


public class DialCombination
{
    public int Position { get; set; }
    public DialDirection Direction { get; set; }

    public DialCombination(int position, DialDirection direction)
    {
        Position = position;
        Direction = direction;
    }
    
}
