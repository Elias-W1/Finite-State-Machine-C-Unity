using System;

public class TimeCondition
{
    private DateTime start;
    private float timelimit;
    private bool fullfilled = true;
    
    public TimeCondition(float tlimit)
    {
        timelimit = tlimit;
    }

    public bool IsFullfilled()
    {

        if (fullfilled)
        {
            start = DateTime.Now;
            fullfilled = false;
            return false;
        }

        if ((DateTime.Now - start).TotalSeconds >= timelimit)
        {
            fullfilled = true;
        }

        return fullfilled;
    }
}
