using System;
using Code.Enums;

public class Corruption
{
    public CorruptionState CorruptionState;

    public void IncreaseCorruptionLevel(int value)
    {
        CorruptionState += value;
        
        if (Enum.IsDefined(typeof(CorruptionState), CorruptionState)) 
            return;
        
        var enumValues = Enum.GetValues(typeof(CorruptionState));
        CorruptionState = (CorruptionState)enumValues.GetValue(enumValues.Length - 1);
    }

    public void DecreaseCorruptionLevel(int value)
    {
        CorruptionState -= value;
        
        if (Enum.IsDefined(typeof(CorruptionState), CorruptionState)) 
            return;
        
        var enumValues = Enum.GetValues(typeof(CorruptionState));
        CorruptionState = (CorruptionState)enumValues.GetValue(0);
    }
}