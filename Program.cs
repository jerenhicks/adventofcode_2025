using System;
using System.Collections.Generic;




class Program
{
    static void Main(string[] args)
    {
        List<Day> days = new List<Day>();
        days.Add(new Day1());
        days.Add(new Day2());
        days.Add(new Day2b());
        days.Add(new Day3a());
        days.Add(new Day3b());
        days.Add(new Day4a());
        days.Add(new Day4b());
        foreach (var day in days)
        {
            Console.WriteLine($"--- {day.GetIdentifier()} ---");
            day.Execute();
            Console.WriteLine(day.GetResult());
            Console.WriteLine($"Elapsed Time: {day.GetElapsedTime()} ms");
            Console.WriteLine();
        }
    }

}


