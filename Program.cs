using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace MyDotNetConsoleApp
{
    class Program
    {
        private static string dataFilePath = @"data-files/day1/star1/testdata/data.txt";
        private static string filePath = @"data-files/day1/star1/data.txt";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            var combinations = ReadDialCombinations(filePath);
            foreach (var combo in combinations)
            {
                Console.WriteLine($"{combo.Direction} {combo.Position}");
            }

            Console.WriteLine("Processing Code...");
            int value = ProcessCode(50, 99, combinations);
            Console.WriteLine($"Final Value: {value}");
        }


        static List<DialCombination> ReadDialCombinations(string filePath)
        {
            var list = new List<DialCombination>();
            foreach (var line in System.IO.File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.Length < 2) continue;
                var dirChar = line[0];
                var numPart = line.Substring(1);
                if (!int.TryParse(numPart, out int pos)) continue;
                DialDirection dir = dirChar == 'L' ? DialDirection.Left : DialDirection.Right;
                list.Add(new DialCombination(pos, dir));
            }
            return list;
        }

        private static int ProcessCode(int startingValue, int maxValue, List<DialCombination> combinations)
        {
            int returnValue = 0;
            int currentReading = startingValue;
            foreach (var combo in combinations)
            {
                if (combo.Direction == DialDirection.Left)
                {
                    for (int i = 0; i < combo.Position; i++)
                    {
                        currentReading--;
                        if (currentReading == 0) 
                        {
                            returnValue++;
                        }
                        if (currentReading < 0)
                        {
                            currentReading = maxValue;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < combo.Position; i++)
                    {
                        currentReading++;
                        if (currentReading > maxValue)
                        {
                            currentReading = 0;
                        }
                        if (currentReading == 0) 
                        {
                            returnValue++;
                        }
                    }
                }
                // if (currentReading == 0)
                // {
                //     returnValue++;
                // }
            }
            
            return returnValue;
        }
    }
}