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

    private MapRenderer _mapRenderer;

    private void Awake()
    {
        _mapRenderer = new(mapObject, cellPrefab);
    }

    public void Render(Map map)
    {
        _mapRenderer.Render(map, wallTiles, floorTiles, borderTiles);
    }
}