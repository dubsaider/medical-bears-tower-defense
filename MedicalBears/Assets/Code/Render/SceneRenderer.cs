using System.Collections.Generic;
using Code.Core;
using Code.Render;
using UnityEngine;

public class SceneRenderer : MonoBehaviour
{
    [Header("Объекты сцены")]
    [SerializeField] private GameObject mapObject;
    
    [Header("Префабы")]
    [SerializeField] private GameObject cellPrefab;
    
    [Header("Спрайты")]
    [SerializeField] private Sprite[] wallTiles;
    [SerializeField] private Sprite[] floorTiles;
    [SerializeField] private Sprite[] borderTiles;
    
    [SerializeField] private Sprite[] corruptionTiles;

    private MapRenderer _mapRenderer;
    private CorruptionRenderer _corruptionRenderer;

   

    public void Render(Map map)
    {
        _mapRenderer.Render(map, wallTiles, floorTiles, borderTiles);
    }

    private void RenderCellCorruption(MapCell cell)
    {
        _corruptionRenderer.RenderCellCorruption(cell, corruptionTiles);
    }
    
    private void Awake()
    {
        _mapRenderer = new(mapObject, cellPrefab);
        _corruptionRenderer = new();

        CellEventsProvider.CellWasCorrupted += RenderCellCorruption;
        CellEventsProvider.CellWasHealed += RenderCellCorruption;
    }
}