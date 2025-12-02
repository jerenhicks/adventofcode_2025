using System;
using System.Collections.Generic;

public class Day2
{
    private static string dataFilePath = @"data-files/day2/star1/data.txt";

    public Day2()
    {
        List<ProductID> productIDs = ReadValues(dataFilePath);
        double sumOfInvalidIDs = 0;
        foreach (var pid in productIDs)
        {
            for (double i = pid.FirstID; i <= pid.SecondID; i++)
            {
                //easiest check would be to see if the number is odd number of digits
                var numberAsString = i.ToString();
                var numberAsStringLength = numberAsString.Length;
                if (numberAsStringLength % 2 == 0)
                {
                    //split the number in half and compare digits
                    var firstHalf = numberAsString.Substring(0, numberAsStringLength / 2);
                    var secondHalf = numberAsString.Substring(numberAsStringLength / 2);
                    if (firstHalf == secondHalf)
                    {
                        sumOfInvalidIDs += i;
                    }

                }
            }
        }
        Console.WriteLine($"Sum of Invalid Product IDs: {sumOfInvalidIDs}");
    }

    public List<ProductID> ReadValues(string filePath)
    {
        var list = new List<ProductID>();
        foreach (var line in System.IO.File.ReadLines(filePath))
        {
            foreach (var part in line.Split(','))
            {
                var ids = part.Split('-');
                if (ids.Length != 2) continue;
                if (double.TryParse(ids[0], out double firstID) && double.TryParse(ids[1], out double secondID))
                {
                    list.Add(new ProductID { FirstID = firstID, SecondID = secondID });
                }
            }
        }
        return list;
    }


}