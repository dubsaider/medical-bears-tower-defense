using Code.Core;

public class Balance
{
    /// <summary>
    /// Текущее значение баланса
    /// </summary>
    public int Value => _value;
    
    private int _value;

    public Balance(int startValue)
    {
        Add(startValue);
    }

    public void Add(int value)
    {
        if (value == 0) return;
        
        _value += value;
        CoreEventsProvider.BalanceChanged.Invoke(_value);
    }

    public void Subtract(int value)
    {
        if (value == 0) return;
        
        _value -= value;
        if (_value < 0)
            _value = 0; 
        
        CoreEventsProvider.BalanceChanged.Invoke(_value);
    }
}