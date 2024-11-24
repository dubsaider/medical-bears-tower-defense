using System.Collections.Generic;
using Code.Core;
using UnityEngine;

public class Map
{
    public MapCell[,] Field;
    public int Width, Height;

    private readonly Dictionary<Vector2Int, MapCell> _corruptedCells;

    public Map()
    {
        _corruptedCells = new();

        CellsEventsProvider.CellWasCorrupted += AddCorruptedCell;
        CellsEventsProvider.CellWasHealed += RemoveCorruptedCell;
    }
 
    public bool TryGetCell(int x, int y, out MapCell cell)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height || Field is null)
        {
            cell = null;
            return false;
        }
        
        cell = Field[x, y];
        return true;
    }

    public bool TryGetCorruptedCell(Vector2Int vector2Int, out MapCell mapCell)
    {
        return _corruptedCells.TryGetValue(vector2Int, out mapCell);
    }
    
    private void AddCorruptedCell(MapCell cell)
    {
        _corruptedCells.TryAdd(cell.Position, cell);
    }
    
    private void RemoveCorruptedCell(MapCell cell)
    {
        _corruptedCells.Remove(cell.Position);
    }
    
}