using Code.Core;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface Surface2D;

    private void Awake()
    {
        Surface2D = GetComponent<NavMeshSurface>();
        CoreEventsProvider.LevelStarted += Bake;
    }

    private void Bake()
    {
        Surface2D.BuildNavMeshAsync();
    }
}
