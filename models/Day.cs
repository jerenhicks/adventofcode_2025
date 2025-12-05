using System;

public abstract class Day
{
    public abstract string GetResult();
    public abstract double GetElapsedTime();
    public abstract string GetIdentifier();
    public abstract void Execute();

    public abstract string Checksum();
}
