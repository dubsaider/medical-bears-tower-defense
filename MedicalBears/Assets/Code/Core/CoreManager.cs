
using UnityEngine;

public class CoreManager : MonoBehaviour
{
    public static CoreManager Instance; 
    
    [SerializeField] private SceneRenderer sceneRenderer;

    private Map _map;
    private MapGenerator _mapGenerator;

    private void Awake()
    {
        Instance = this;
        _mapGenerator = new();
    }

    private void Start()
    {
        InitLevel();
    }

    private void InitLevel()
    {
        _map = _mapGenerator.Generate(13, 20, 2, 3, Random.Range(20,1000));
        sceneRenderer.Render(_map);
    }
}