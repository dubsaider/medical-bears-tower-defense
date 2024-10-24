using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 30;
    public int mapHeight = 30;
    public int borderWidth = 2;
    public int numberOfSpawnPoints = 3;
    public int seed = 42;

    private Map map;
    private List<Vector2Int> entryPoints;
    private Vector2Int exitPoint;
    private bool[,] visited;

    private void Start()
    {
        Random.InitState(seed);
        map = new Map
        {
            Field = new MapCell[mapWidth, mapHeight],
            Width = mapWidth,
            Height = mapHeight
        };

        visited = new bool[mapWidth, mapHeight];
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                map.Field[x, y] = new MapCell { Type = MapCellType.Walkable, X = x, Y = y };
                visited[x, y] = false;
            }
        }

        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < borderWidth; y++)
            {
                if (y < mapHeight)
                {
                    map.Field[x, y].Type = MapCellType.Border;
                    map.Field[x, mapHeight - 1 - y].Type = MapCellType.Border;
                }
            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < borderWidth; x++)
            {
                if (x < mapWidth)
                {
                    map.Field[x, y].Type = MapCellType.Border;
                    map.Field[mapWidth - 1 - x, y].Type = MapCellType.Border;
                }
            }
        }

        entryPoints = new List<Vector2Int>();
        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            Vector2Int entryPoint;
            do
            {
                entryPoint = new Vector2Int(Random.Range(borderWidth, mapWidth - borderWidth), Random.Range(borderWidth, mapHeight - borderWidth));
            } while (entryPoints.Contains(entryPoint) || !IsFree(entryPoint.x, entryPoint.y));
            entryPoints.Add(entryPoint);
        }
        do
        {
            exitPoint = new Vector2Int(Random.Range(borderWidth, mapWidth - borderWidth), mapHeight - 1 - borderWidth);
        } while (entryPoints.Contains(exitPoint) || !IsFree(exitPoint.x, exitPoint.y));

        int numberOfElevationTiles = (mapWidth * mapHeight) / 4;
        for (int i = 0; i < numberOfElevationTiles; i++)
        {
            Vector2Int elevationPoint;
            do
            {
                elevationPoint = new Vector2Int(Random.Range(borderWidth, mapWidth - borderWidth), Random.Range(borderWidth, mapHeight - borderWidth));
            } while (!IsFree(elevationPoint.x, elevationPoint.y));
            map.Field[elevationPoint.x, elevationPoint.y].Type = MapCellType.Wall;
        }

        foreach (var entryPoint in entryPoints)
        {
            map.Field[entryPoint.x, entryPoint.y].Type = MapCellType.Walkable;
        }
        map.Field[exitPoint.x, exitPoint.y].Type = MapCellType.Walkable;

        BFS();
        FillUnvisitedTiles();
        CheckElevationTilesForNeighbors();
    }

    private bool IsFree(int x, int y)
    {
        if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
        {
            if (map.Field[x, y].Type != MapCellType.Walkable)
            {
                return false;
            }
        }
        return true;
    }

    private void BFS()
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        foreach (var entryPoint in entryPoints)
        {
            queue.Enqueue(entryPoint);
            visited[entryPoint.x, entryPoint.y] = true;
        }

        queue.Enqueue(exitPoint);
        visited[exitPoint.x, exitPoint.y] = true;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            foreach (var neighbor in GetNeighbors(current))
            {
                if (!visited[neighbor.x, neighbor.y] && IsWalkable(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor.x, neighbor.y] = true;
                }
            }
        }
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            Vector2Int neighbor = new Vector2Int(position.x + dx[i], position.y + dy[i]);
            if (neighbor.x >= 0 && neighbor.x < mapWidth && neighbor.y >= 0 && neighbor.y < mapHeight)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    private bool IsWalkable(Vector2Int position)
    {
        return map.Field[position.x, position.y].Type == MapCellType.Walkable;
    }

    private void FillUnvisitedTiles()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (!visited[x, y] && map.Field[x, y].Type == MapCellType.Walkable && IsSurroundedByElevation(new Vector2Int(x, y)))
                {
                    map.Field[x, y].Type = MapCellType.Wall;
                }
            }
        }
    }

    private bool IsSurroundedByElevation(Vector2Int position)
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            Vector2Int neighbor = new Vector2Int(position.x + dx[i], position.y + dy[i]);
            if (neighbor.x >= 0 && neighbor.x < mapWidth && neighbor.y >= 0 && neighbor.y < mapHeight)
            {
                if (map.Field[neighbor.x, neighbor.y].Type != MapCellType.Wall)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void CheckElevationTilesForNeighbors()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (map.Field[x, y].Type == MapCellType.Wall && !HasNeighbors(new Vector2Int(x, y)))
                {
                    map.Field[x, y].Type = MapCellType.Walkable;
                }
            }
        }
    }

    private bool HasNeighbors(Vector2Int position)
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            Vector2Int neighbor = new Vector2Int(position.x + dx[i], position.y + dy[i]);
            if (neighbor.x >= 0 && neighbor.x < mapWidth && neighbor.y >= 0 && neighbor.y < mapHeight)
            {
                if (map.Field[neighbor.x, neighbor.y].Type == MapCellType.Wall)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Map GetMap()
    {
        return map;
    }
}