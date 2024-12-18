using Code.Entities.Map;
using UnityEngine;

public class MapCell
{
    public MapCellType Type;
    public Vector2Int Position;

    /// <summary>
    /// Обработчик строительства на клетке
    /// </summary>
    public CellHandler CellHandler;
    
    /// <summary>
    /// Обработчик заражения клетки
    /// </summary>
    public CellCorruptionHandler CorruptionHandler;
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