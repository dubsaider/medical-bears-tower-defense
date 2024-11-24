using System.Collections;
using Code.Core;
using Code.Entities.Map;
using UnityEngine;

public class CorruptionAttack : MonoBehaviour
{
    [Header("Сила заражения клеток")]
    [Range(-5,5)]
    [SerializeField] protected int corrupForce;
    
    // инициализатор
    private void Start()
    {
        StartCoroutine(CorruptionTimeCycle());
    }

    // логика заражения
    private void DoCorruption()
    {
        var x = Mathf.RoundToInt(transform.position.x);
        var y = Mathf.RoundToInt(transform.position.y);

        var cell = CoreManager.Instance.GetCell(x, y);
        if (!cell) 
            return;
        
        //мы обращаемся к объекту клетки и обращаемся к классу обработчика заражений со запросом на увеличение заражения
        switch (corrupForce)
        {
            case >= 0:
                cell.GetComponent<CellCorruptionHandler>().IncreaseCorruptionLevel(corrupForce);
                break;
            case < 0:
                cell.GetComponent<CellCorruptionHandler>().DecreaseCorruptionLevel(-corrupForce);
                break;
        }
    }

    //таймаут между заражением клеток
    IEnumerator CorruptionTimeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            DoCorruption();
        }
    }
}
