using System;

public class Battery
{
    public int capacity;
    public Guid Id = Guid.NewGuid();
    public bool isUsed = false;
}