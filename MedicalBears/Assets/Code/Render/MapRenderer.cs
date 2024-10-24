using System.Linq;
using Code.Entities.Map;
using Extensions;
using UnityEngine;

namespace Code.Render
{
    public class MapRenderer
    {
        private readonly GameObject _parentObject;
        private readonly GameObject _cellPrefab;
        public MapRenderer(GameObject parentObj, GameObject cellPrefab)
        {
            _parentObject = parentObj;
            _cellPrefab = cellPrefab;
        }
        
        public void Render(Map map, Sprite[] wallTiles, Sprite[] floorTiles, Sprite[] borderTiles)
        {
            RenderWalls(map, wallTiles);
            RenderFloors(map, floorTiles);
            RenderBorders(map, borderTiles);
        }

        private void RenderWalls(Map map, Sprite[] tiles)
        {
            foreach (var cell in map.Field)
            {
                if (cell.Type is MapCellType.Wall)
                    RenderCell(cell, tiles.GetRandomItem()); //TODO доделать в будущем (красиво формировать большие фигуры тайлами)
            }
        }
        
        private void RenderFloors(Map map, Sprite[] tiles)
        {
            foreach (var cell in map.Field)
            {
                if (cell.Type is MapCellType.Floor)
                    RenderCell(cell, tiles.GetRandomItem());
            }
        }

        private void RenderBorders(Map map, Sprite[] tiles)
        {
            foreach (var cell in map.Field)
            {
                if (cell.Type is MapCellType.Border)
                    RenderCell(cell, tiles.GetRandomItem());
            }
        }

        private void RenderCell(MapCell cell, Sprite sprite)
        {
            var cellObj = ObjectsManager.CreateObject(_cellPrefab, _parentObject, new Vector3(cell.X, cell.Y, 0));
            cellObj.GetComponentInChildren<SpriteRenderer>().sprite = sprite;

            cellObj.GetComponent<CellHandler>().SetCell(cell); //биндинг сущности к вьюхе
        }
    }
}