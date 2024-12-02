using Code.Core;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

class MyCell
{
    public int id;
    public MapCell mapCell;
    public bool canUse;
}

public class HeallerUnitSummonner : MonoBehaviour
{
    [SerializeField] int MaxUnitCount;
    [SerializeField] int SpawnRange;
    [SerializeField] int SpawnUnitTime;
    [SerializeField] bool CanRespawnNewUnit;
    [SerializeField] GameObject UnitPrefab;

    private List<MyCell> Cells;

    private int _currentUnitCnt;

    private bool CellsIsDone;

    void Start()
    {
        CoreEventsProvider.HealerUnitHasDie += ReduceUnitCount;

        Cells = new List<MyCell>();

        StartCoroutine(StartCourutine());

        DestroyObjectsHandler.Add(gameObject);
    }


    private void ReduceUnitCount(int id)
    {
        _currentUnitCnt--;
        
        if (TryGetID(id, out int ind))
        {
            Cells[ind].id = -1;
            Cells[ind].mapCell = null;
            Cells[ind].canUse = true;
            Cells[ind].mapCell.CellHandler.IsUnitStayOnCell = false;
        }

        StartCoroutine(SpawnUnitsTimer());

    }

    private void SummonUnits()
    {
        CheckNearCells();

        int dictInd;
        int ID;

        //защита от взятия уже использрванной ячейки
        do
        {
            dictInd = Random.Range(0, Cells.Count);
        }
        while (!Cells[dictInd].mapCell.CellHandler.IsEmpty);

        //защита от повторных индексов
        do
        {
            ID = Random.Range(1, int.MaxValue);
        }
        while (TryFoundID(ID));

        GameObject Unit = Instantiate(UnitPrefab, new Vector3(Cells[dictInd].mapCell.Position.x, Cells[dictInd].mapCell.Position.y), new Quaternion(0, 0, 0, 0));

        Unit.GetComponent<FriendlyUnit>().SetID(ID);

        Cells[dictInd].id = ID;
        Cells[dictInd].canUse = false;

        Cells[dictInd].mapCell.CellHandler.IsUnitStayOnCell = true;

        _currentUnitCnt++;
    }

    private void CheckNearCells()
    {
        var Mx = Mathf.RoundToInt(transform.position.x);
        var My = Mathf.RoundToInt(transform.position.y);

        //сначала проверяем на каких клетках можно ставить юнита

        for (int x = -SpawnRange; x <= SpawnRange; x++)
        {
            for (int y = -SpawnRange; y <= SpawnRange; y++)
            {
                if (CoreManager.Instance.Map.TryGetCell(Mx + x, My + y, out MapCell cell )
                    && (x != Mx && y != My)
                    && CoreManager.Instance.Map.Field[Mx + x, My + y].CellHandler.IsEmpty)
                {
                    if (cell.Type == MapCellType.Floor)
                    {
                        Cells.Add(new MyCell
                        {
                            id = -1,
                            mapCell = cell,
                            canUse = true,
                        });

                        cell.CellHandler.IsUnitStayOnCell = false;
                    }
                }
            }
        }

        CellsIsDone = true;
    }

    IEnumerator SpawnUnitsTimer()
    {
        _currentUnitCnt++;
        if (_currentUnitCnt <= MaxUnitCount && CanRespawnNewUnit)
        {
            yield return new WaitForSeconds(SpawnUnitTime);
            _currentUnitCnt--;
            SummonUnits();
            StartCoroutine(SpawnUnitsTimer());
        }
    }

    IEnumerator StartCourutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (gameObject.CompareTag("Tower")) { break; }
        }

        CheckNearCells();

        yield return new WaitUntil(() => CellsIsDone);

        SummonUnits();

        StartCoroutine(SpawnUnitsTimer());
    }

    private bool TryFoundID(int id)
    {
        foreach (MyCell cell in Cells)
        {
            if (cell.id == id)
            {
                return true;
            }
        }
        
        return false;
    }

    private bool TryGetID(int id, out int ind)
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            if (Cells[i].id == id)
            {
                ind = i;
                return true;
            }
        }
        ind = 0;
        return false;
    }

    void OnDestroy()
    {
        CoreEventsProvider.HealerUnitHasDie -= ReduceUnitCount;
        StopAllCoroutines();
    }

}
