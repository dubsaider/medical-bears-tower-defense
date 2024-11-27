using Code.Core;
using UnityEngine;

public class CorruptionObserver : MonoBehaviour
{
    public static int MaxCorruptedCells {  get; private set; }
    public static int CurrentCorruptedCells {  get; private set; }

    private Map map;

    private int _CellsCnt = 0;
    private int _width,_height;

    void Start()
    {
        map = CoreManager.Instance.Map;

        _width = map.Width;
        _height = map.Height;

        if (FindCells(out int val))
        {
            MaxCorruptedCells = val;
            Debug.Log(val);
        }
    }

    private void CorruptionEventHandler()
    {

    }

    private void CurrentCorruptedCellslUpdate()
    {

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

        val = cnt;

        if (cnt > 0)

        {
            return true;
        }
        else
        {
            return false;
        }

    }
    
}
