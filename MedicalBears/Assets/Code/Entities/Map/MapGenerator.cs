using UnityEngine;
using System.Collections.Generic;

public class MapGenerator
{
    private int _width;
    private int _height;
    private int _borderWidth;
    private int _spawnPointsCount;

    private Map _map;
    private List<Vector2Int> entryPoints;
    private Vector2Int exitPoint;
    private bool[,] visited;

    public Map Generate(int width, int height, int borderWidth, int spawnPointsCount, int seed)
    {
        Random.InitState(seed);

        _width = width;
        _height = height;
        _borderWidth = borderWidth;
        _spawnPointsCount = spawnPointsCount;

        return GenerateMap();
    }

    private Map GenerateMap()
    {
        _map = new Map
        {
            Field = new MapCell[_width, _height],
            Width = _width,
            Height = _height
        };

        visited = new bool[_width, _height];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _map.Field[x, y] = new MapCell { Type = MapCellType.Floor, X = x, Y = y };
                visited[x, y] = false;
            }
        }

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _borderWidth; x++)
            {
                if (x < _width)
                {
                    _map.Field[x, y].Type = MapCellType.Border;
                    _map.Field[_width - 1 - x, y].Type = MapCellType.Border;
                }
            }
        }

        entryPoints = new List<Vector2Int>();
        for (int i = 0; i < _spawnPointsCount; i++)
        {
            Vector2Int entryPoint;
            do
            {
                entryPoint = new Vector2Int(Random.Range(_borderWidth, _width - _borderWidth), _height - 1 - _borderWidth);
            } while (entryPoints.Contains(entryPoint) || !IsFree(entryPoint.x, entryPoint.y));
            entryPoints.Add(entryPoint);
            _map.Field[entryPoint.x, entryPoint.y].Type = MapCellType.Spawner;
        }
        do
        {
            exitPoint = new Vector2Int(Random.Range(_borderWidth, _width - _borderWidth), _height - 1 - _borderWidth);
        } while (entryPoints.Contains(exitPoint) || !IsFree(exitPoint.x, exitPoint.y));

        int numberOfElevationTiles = (_width * _height) / 4;
        for (int i = 0; i < numberOfElevationTiles; i++)
        {
            Vector2Int elevationPoint;
            do
            {
                elevationPoint = new Vector2Int(Random.Range(_borderWidth, _width - _borderWidth), Random.Range(_borderWidth, _height - _borderWidth));
            } while (!IsFree(elevationPoint.x, elevationPoint.y));
            _map.Field[elevationPoint.x, elevationPoint.y].Type = MapCellType.Wall;
        }

        foreach (var entryPoint in entryPoints)
        {
            _map.Field[entryPoint.x, entryPoint.y].Type = MapCellType.Floor;
        }
        _map.Field[exitPoint.x, exitPoint.y].Type = MapCellType.Floor;

        BFS();
        FillUnvisitedTiles();
        CheckElevationTilesForNeighbors();

        return _map;
    }

    private bool IsFree(int x, int y)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            if (_map.Field[x, y].Type != MapCellType.Floor)
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
            if (neighbor.x >= 0 && neighbor.x < _width && neighbor.y >= 0 && neighbor.y < _height)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    private bool IsWalkable(Vector2Int position)
    {
        return _map.Field[position.x, position.y].Type == MapCellType.Floor;
    }

    private void FillUnvisitedTiles()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (!visited[x, y] && _map.Field[x, y].Type == MapCellType.Floor && IsSurroundedByElevation(new Vector2Int(x, y)))
                {
                    _map.Field[x, y].Type = MapCellType.Wall;
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
            if (neighbor.x >= 0 && neighbor.x < _width && neighbor.y >= 0 && neighbor.y < _height)
            {
                if (_map.Field[neighbor.x, neighbor.y].Type != MapCellType.Wall)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void CheckElevationTilesForNeighbors()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_map.Field[x, y].Type == MapCellType.Wall && !HasNeighbors(new Vector2Int(x, y)))
                {
                    _map.Field[x, y].Type = MapCellType.Floor;
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
            if (neighbor.x >= 0 && neighbor.x < _width && neighbor.y >= 0 && neighbor.y < _height)
            {
                if (_map.Field[neighbor.x, neighbor.y].Type == MapCellType.Wall)
                {
                    return true;
                }
            }
        }

        return false;
    }
}