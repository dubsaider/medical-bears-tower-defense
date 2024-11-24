using System;
using Code.Enums;

// класс хранит в себе значение заражения и методы для прибавления / убавления значений
public class Corruption
{
    public CorruptionState CorruptionState;

    // добавление значения к "CorruptionState"
    public void IncreaseCorruptionLevel(int value)
    {
        /*
         * проверка существует ли "CorruptionState + value" в "CorruptionState"
         * если существует, заканчиваем метод,  если не существует, ставим максимальное значение из енама
        */
        CorruptionState += value;

        if (Enum.IsDefined(typeof(CorruptionState), CorruptionState)) 
            return;
        
        var enumValues = Enum.GetValues(typeof(CorruptionState));
        CorruptionState = (CorruptionState)enumValues.GetValue(enumValues.Length - 1);
    }

    // убавление значения у "CorruptionState"
    public void DecreaseCorruptionLevel(int value)
    {
        CorruptionState -= value;
     
        if (Enum.IsDefined(typeof(CorruptionState), CorruptionState)) 
            return;
        
        var enumValues = Enum.GetValues(typeof(CorruptionState));
        CorruptionState = (CorruptionState)enumValues.GetValue(0);
    }
}