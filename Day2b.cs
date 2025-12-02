using System;
using System.Collections.Generic;
using System.Linq;


public class Day2b
{
    private static string dataFilePath = @"data-files/day2/star1/data.txt";

    public Day2b()
    {
        List<ProductID> productIDs = ReadDialCombinations(dataFilePath);
        double sumOfInvalidIDs = 0;
        foreach (var pid in productIDs)
        {
            for (double i = pid.FirstID; i <= pid.SecondID; i++)
            {
                //easiest check would be to see if the number is odd number of digits
                var numberAsString = i.ToString();
                var numberAsStringLength = numberAsString.Length;
                for (int x = 2; x <= numberAsStringLength; x++)
                {
                    //can we split the number into x equal parts?
                    if (numberAsStringLength % x == 0)
                    {
                        var partLength = numberAsStringLength / x;
                        var digits = new List<int>();
                        for (int p = 0; p < x; p++)
                        {
                            var part = numberAsString.Substring(p * partLength, partLength);
                            digits.Add(int.Parse(part));
                        }

                        //all the values must be the same to be 'valid'
                        if (digits.Distinct().Count() == 1)
                        {
                            //Console.WriteLine("Invalid ID Found: " + i);
                            sumOfInvalidIDs += i;
                            break;
                        }
                    }
                }
            }
        }
        Console.WriteLine($"Sum of Invalid Product IDs: {sumOfInvalidIDs}");
    }

    public List<ProductID> ReadDialCombinations(string filePath)
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