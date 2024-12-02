using System.Collections.Generic;
using Code.Core;
using Unity.VisualScripting;
using UnityEngine;

public class Map
{
    public MapCell[,] Field;
    public int Width, Height;

    private readonly Dictionary<Vector2Int, MapCell> _corruptedCells;

    public Map()
    {
        _corruptedCells = new();

        CellEventsProvider.CellWasCorrupted += AddCorruptedCell;
        CellEventsProvider.CellWasHealed += RemoveCorruptedCell;
    }

    public MapCell GetRandomFreeFloorCell()
    {
        do
        {
            var randX = Random.Range(0, Width);
            var randY = Random.Range(0, Height);

            if (!TryGetCell(randX, randY, out var cell)) 
                continue;

            if (cell.Type is MapCellType.Floor && cell.CellHandler.IsEmpty)
                return cell;

        } while (true);
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
    
    void OnDestroy()
    {
        CellEventsProvider.CellWasCorrupted -= AddCorruptedCell;
        CellEventsProvider.CellWasHealed -= RemoveCorruptedCell;
    }

}