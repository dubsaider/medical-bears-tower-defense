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

public class HeallerUnitSummonner : MonoBehaviour, ITowerAttackComponent
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
            Cells[ind].canUse = true;
            Cells[ind].mapCell.CellHandler.IsUnitStayOnCell = false;
        }

    }

    private bool HasFreeCells(out int val)
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            if (Cells[i].canUse) 
            {
                val = i;
                return true;
            }
        }
        val = 0;
        return false;
    }

    private void SummonUnits()
    {
        int dictInd = 0;
        int ID;

        //защита от взятия уже использрванной ячейки

        if (HasFreeCells(out int val))
        {
            dictInd = val;
        }
        else
        {
            return;
        }
        

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
        while (true)
        {
            if (_currentUnitCnt < Cells.Count && _currentUnitCnt < MaxUnitCount && CanRespawnNewUnit)
            {
                yield return new WaitForSeconds(SpawnUnitTime);
                SummonUnits();
                _currentUnitCnt++;

            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator StartCourutine()
    {

        yield return new WaitUntil(() => gameObject.CompareTag("Tower"));

        CheckNearCells();

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

    public void Attack(Transform firePoint, float range, float damage)
    {
        return;
        //throw new System.NotImplementedException();
    }
}
