using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell
{
    public MapCellType Type;
    public int X, Y;

    /// <summary>
    /// Отрисованная в сцене клетка
    /// </summary>
    public GameObject RenderedObject;
}

public enum MapCellType
{
    /// <summary>
    /// Проход
    /// </summary>
    Floor = 0,
    /// <summary>
    /// Стена, которую обходят челики
    /// </summary>
    Wall = 1,
    /// <summary>
    /// Граница (недоступная)
    /// </summary>
    Border = 2,
    /// <summary>
    /// Спавнер мобов
    /// </summary>
    Spawner = 3,
    /// <summary>
    /// Зараженная клетка
    /// </summary>
    CorruptCell = 4
}