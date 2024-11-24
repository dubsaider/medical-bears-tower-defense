using System;
using Code.Enums;

// класс хранит в себе значение заражения и методы для прибавления / убавления значений
public class Corruption
{
    public CorruptionState CorruptionState;
    public CorruptionState MaxCorruptionLevel => GetMaxCorruptionLevel();

    public bool IsMaxCorrupted => CorruptionState == MaxCorruptionLevel;
    public bool IsHealthy => CorruptionState is CorruptionState.NotCorrupted;

    /// <summary>
    /// Увеличение уровня заражения
    /// </summary>
    public void IncreaseCorruptionLevel(int value)
    {
        /*
         * проверка существует ли "CorruptionState + value" в "CorruptionState"
         * если существует, заканчиваем метод,  если не существует, ставим максимальное значение из енама
        */
        CorruptionState += value;

        if (Enum.IsDefined(typeof(CorruptionState), CorruptionState)) 
            return;
        
        CorruptionState = MaxCorruptionLevel;
    }

    /// <summary>
    /// Уменьшение уровня заражения
    /// </summary>
    public void DecreaseCorruptionLevel(int value)
    {
        CorruptionState -= value;
     
        if (Enum.IsDefined(typeof(CorruptionState), CorruptionState)) 
            return;
        
        CorruptionState = CorruptionState.NotCorrupted;
    }

    private CorruptionState GetMaxCorruptionLevel()
    {
        var enumValues = Enum.GetValues(typeof(CorruptionState));
        return (CorruptionState)enumValues.GetValue(enumValues.Length - 1);
    }
}