using System;
using System.Collections.Generic;

public class SquidEquation
{
    List<List<double>> numbers = new List<List<double>>();
    Operation operation;
    public int significantDigits = 0;

    public void AddNumber(List<double> number)
    {
        //numbers.Add(number);
        //add to the top of the list
        numbers.Insert(0, number);
    }

    public void SetOperation(Operation op)
    {
        operation = op;
    }

    public void SetSignificantDigits(int digits)
    {
        significantDigits = digits;
    }

    // public double Evaluate()
    // {
    //     if (numbers.Count == 0) return 0;

    //     double result = numbers[0];

    //     for (int i = 1; i < numbers.Count; i++)
    //     {
    //         switch (operation)
    //         {
    //             case Operation.Add:
    //                 result += numbers[i];
    //                 break;
    //             case Operation.Subtract:
    //                 result -= numbers[i];
    //                 break;
    //             case Operation.Multiply:
    //                 result *= numbers[i];
    //                 break;
    //             case Operation.Divide:
    //                 if (numbers[i] != 0)
    //                 {
    //                     result /= numbers[i];
    //                 }
    //                 else
    //                 {
    //                     throw new DivideByZeroException("Cannot divide by zero.");
    //                 }
    //                 break;
    //         }
    //     }

    //     return result;
    // }

    public double SquidMathEvaluate()
    {
        List<double> realValues = new List<double>();
        var stringValue = "";
        for (int i = 0; i <= significantDigits - 1; i++)
        {
            foreach (var numList in numbers)
            {
                if (numList[i] != -1)
                {
                    stringValue += numList[i].ToString();
                }
            }
            realValues.Add(double.Parse(stringValue));
            stringValue = "";
        }
        var result = realValues[0];
        for (int i = 1; i < realValues.Count; i++)
        {
            switch (operation)
            {
                case Operation.Add:
                    result += realValues[i];
                    break;
                case Operation.Subtract:
                    result -= realValues[i];
                    break;
                case Operation.Multiply:
                    result *= realValues[i];
                    break;
                case Operation.Divide:
                    if (realValues[i] != 0)
                    {
                        result /= realValues[i];
                    }
                    else
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    break;
            }
        }

        return result;


    }

}