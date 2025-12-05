using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;


public class InventoryRange
{
    public double min { get; set; }
    public double max { get; set; }

    public InventoryRange(double min, double max)
    {
        this.min = min;
        this.max = max;
    }

    public bool IsInRange(double value)
    {
        return value >= min && value <= max;
    }

    public List<double> GetRangeSize()
    {
        List<double> rangeValues = new List<double>();
        for (double i = min; i <= max; i++)
        {
            rangeValues.Add(i);
        }
        return rangeValues;
    }

    public double GetRangeSizeCount()
    {
        return (double)(max - min + 1);
    }

    public Boolean Overlaps(InventoryRange other)
    {
        return this.min <= other.max && other.min <= this.max;
    }

    public Boolean Adjacent(InventoryRange other)
    {
        return this.max + 1 == other.min || other.max + 1 == this.min;
    }

    public InventoryRange Merge(InventoryRange other)
    {
        double newMin = Math.Min(this.min, other.min);
        double newMax = Math.Max(this.max, other.max);
        return new InventoryRange(newMin, newMax);
    }

}
