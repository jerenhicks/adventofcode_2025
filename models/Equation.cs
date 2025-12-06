using System;
using System.Collections.Generic;

public class Equation
{
    List<double> numbers = new List<double>();
    Operation operation;

    public void AddNumber(double number)
    {
        numbers.Add(number);
    }

    public void SetOperation(Operation op)
    {
        operation = op;
    }

    public double Evaluate()
    {
        if (numbers.Count == 0) return 0;

        double result = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            switch (operation)
            {
                case Operation.Add:
                    result += numbers[i];
                    break;
                case Operation.Subtract:
                    result -= numbers[i];
                    break;
                case Operation.Multiply:
                    result *= numbers[i];
                    break;
                case Operation.Divide:
                    if (numbers[i] != 0)
                    {
                        result /= numbers[i];
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