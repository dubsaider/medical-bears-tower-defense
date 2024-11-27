using System.Linq;
using Code.Entities.Map;
using Extensions;
using NavMeshPlus.Components;
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
            
            NavMeshBaker.Instance.Bake();
        }

        private void RenderWalls(Map map, Sprite[] tiles)
        {
            foreach (var cell in map.Field)
            {
                if (cell.Type is MapCellType.Wall)
                    RenderCell(cell, tiles.GetRandomItem(), "Wall", RotationRandomizer.GetRandom90DegreesRotation());
            }
        }
        
        private void RenderFloors(Map map, Sprite[] tiles)
        {
            foreach (var cell in map.Field)
            {
                if (cell.Type is MapCellType.Floor)
                    RenderCell(cell, tiles.GetRandomItem(), "Floor");
            }
        }

        private void RenderBorders(Map map, Sprite[] tiles)
        {
            foreach (var cell in map.Field)
            {
                if (cell.Type is MapCellType.Border)
                    RenderCell(cell, tiles.GetRandomItem(), "Wall", RotationRandomizer.GetRandom90DegreesRotation());
            }
        }

        private void RenderCell(MapCell cell, Sprite sprite, string layerName, Quaternion rotation = default)
        {
            var cellObj = ObjectsManager.CreateObject(_cellPrefab, _parentObject, (Vector3Int)cell.Position, rotation);
            cellObj.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            cellObj.layer = LayerMask.NameToLayer(layerName);

            cell.CorruptionHandler = cellObj.GetComponent<CellCorruptionHandler>();
            cell.CorruptionHandler.Init(cell);
            
            cell.CellHandler = cellObj.GetComponent<CellHandler>();
            cell.CellHandler.Init(cell);
            
            if (cell.Type is MapCellType.Border)
                cell.CorruptionHandler.enabled = false;

            SetupNavMesh(cell, cellObj);
        }

        private void SetupNavMesh(MapCell cell, GameObject cellObj)
        {
            var navMeshModifier = cellObj.AddComponent<NavMeshModifier>();
            navMeshModifier.overrideArea = true;
            
            navMeshModifier.area = cell.Type == MapCellType.Floor ? 0 : 1;
        }
    }
}