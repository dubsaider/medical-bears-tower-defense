using Code.Core;
using Code.Render;
using UnityEngine;

public class SceneRenderer : MonoBehaviour
{
    [Header("Объекты сцены")]
    [SerializeField] private GameObject mapObject;
    [SerializeField] private GameObject spawnAreaObject;
    
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
        spawnAreaObject.transform.position = new Vector3(
            (float)map.Width / 2 - 0.5f, 
            map.Height + 5f, 
            spawnAreaObject.transform.position.z);
        
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