using Code.Core;
using UnityEngine;

public class CorruptionObserver : MonoBehaviour
{
    [SerializeField] int MaxCorruptionProcents;

    private static int _maxCorruption;
    private static int _currentCorruption;

    private void CorruptionEventHandler(int value)
    {
        if (IsCorrectCorrupLevel(value, out int refVal, out int procValue))
            _currentCorruption = refVal;
        
        CoreEventsProvider.TotalCorruptionValueUpdated.Invoke(procValue);
    }

    private bool IsCorrectCorrupLevel(int value, out int refVal, out int procValue)
    {
        float curVal = _currentCorruption + value;

        int res = (int)(curVal / _maxCorruption * 100);

        if (res > 0 && res < 100)
        {
            refVal = (int)curVal;
            procValue = res;
        }
        else if (res <= 0)
        {
            refVal = (int)curVal;
            procValue = 0;
        }
        else
        {
            refVal = 100;
            procValue = 100;
            CoreEventsProvider.LevelNotPassed.Invoke();
        }

        return true;
    }

    private bool FindCells(out int val)
    {
        var map = CoreManager.Instance.Map;

        var width = map.Width;
        var height = map.Height;
        
        int cnt=0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map.TryGetCell(x,y, out MapCell mapcell))
                {
                    if (mapcell.Type == MapCellType.Floor || mapcell.Type == MapCellType.Wall)
                    {
                        cnt++;
                    }
                }
            }
        }

        val = cnt * 5;
        return cnt > 0;
    }
    
    private void Init()
    {
        if (FindCells(out int val))
        {
            _maxCorruption = val;
            Debug.Log(val);
        }

        _currentCorruption = 0;
    }
    
    private void Awake()
    {
        CoreEventsProvider.LevelStarted += Init;
        CellEventsProvider.AddValueToCorruptionLevel += CorruptionEventHandler;
    }

    private void OnDestroy()
    {
        CoreEventsProvider.LevelStarted -= Init;
        CellEventsProvider.AddValueToCorruptionLevel -= CorruptionEventHandler;
    }
}
