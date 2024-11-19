using UnityEngine;

public class OrangeEnemy : Enemy
{
    [SerializeField] private float corruptionRadius = 2f;
    [SerializeField] private MapCellType corruptedCellType = MapCellType.CorruptCell;

    private Map map;

    void Start()
    {
        map = FindObjectOfType<Map>();
    }

    private void CorruptCellAtPosition(Vector3 position)
    {
        if (map == null) return;

        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        // TODO: Перерисовывать карту после заражения 
    }
}