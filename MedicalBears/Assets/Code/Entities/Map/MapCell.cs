using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell
{
    public MapCellType Type;
    public int X, Y;
}

public enum MapCellType
{
    /// <summary>
    /// Граница (недоступная)
    /// </summary>
    Border = 0,
    
    /// <summary>
    /// Стена, которую обходят челики
    /// </summary>
    Wall = 1,
    
    /// <summary>
    /// Проход
    /// </summary>
    Walkable = 2
}