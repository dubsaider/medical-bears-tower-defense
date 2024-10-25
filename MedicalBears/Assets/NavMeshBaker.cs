using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    public static NavMeshBaker Instance;
    
    private NavMeshSurface Surface2D;

    private void Awake()
    {
        Instance = this;
        Surface2D = GetComponent<NavMeshSurface>();
    }

    public void Bake()
    {
        Surface2D.BuildNavMeshAsync();
    }
}
