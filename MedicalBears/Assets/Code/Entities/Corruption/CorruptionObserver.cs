using Code.Core;
using System;
using UnityEngine;

public class CorruptionObserver : MonoBehaviour
{
    [SerializeField] int MaxCorruptionProcents;   

    public static int MaxCorruptedCells {  get; private set; }
    public static int CurrentCorruptedCells {  get; private set; }

    private Map map;

    private int _CellsCnt = 0;
    private int _width,_height;

    void Start()
    {
        CellEventsProvider.AddValueToCorruptionLevel += CorruptionEventHandler;
;
        map = CoreManager.Instance.Map;

        _width = map.Width;
        _height = map.Height;

        if (FindCells(out int val))
        {
            MaxCorruptedCells = val;
            Debug.Log(val);
        }
    }

    private void CorruptionEventHandler(int val)
    {

        if (IsCorrectCorrupLevel(val, out int refVal, out int procValue))
        {
            CurrentCorruptedCells = refVal;
        }
        
        CurrentCorruptedCellslUpdate(procValue);
    }

    private bool IsCorrectCorrupLevel(int val, out int refVal, out int procValue)
    {
        float curVal = CurrentCorruptedCells + val;

        int res = (int)(curVal / MaxCorruptedCells * 100);

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
            CoreEventsProvider.CriticalCorruptionReached.Invoke();
        }

        return true;
    }

    private void CurrentCorruptedCellslUpdate(int procValue)
    {
        CoreEventsProvider.TotalCorruptionValueUpdated.Invoke(procValue);
    }

    private bool FindCells(out int val)
    {
        int cnt=0;

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
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

        val = cnt*5;

        if (cnt > 0)

        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnDestroy()
    {
        CellEventsProvider.AddValueToCorruptionLevel -= CorruptionEventHandler;
    }
}
