using Code.Core;
using Code.Entities.Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorruptionAttack : MonoBehaviour
{
    [Header("���� ��������� ������")]
    [Range(-5,5)]
    [SerializeField] protected int corrupForce;

    private static CellHandler[,] cellHandlers;
    private Vector2 position;
    private Hero hero;
    private int m_x, m_y;

    // �������������
    public void Start()
    {
        hero = GetComponent<Hero>();

        CellFinder();

        StartCoroutine("CorupTimeCycle");
    }

    // ����� ������
    private void CellFinder()
    {
        if (cellHandlers == null)
        { 
            List<CellHandler> CellHandlerMassive;
            int width = 0, height = 0;
            int q = 0;

            width = FindObjectOfType<CoreManager>().GetWidth();
            height = FindObjectOfType<CoreManager>().GetHeight();

            CellHandlerMassive = FindObjectsOfType<CellHandler>().ToList<CellHandler>();

            var sortedCellMassive = CellHandlerMassive.OrderByDescending(_y => -_y.GetY()).ThenBy(_x=>_x.GetX()).ToList();

            cellHandlers = new CellHandler[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cellHandlers[i, j] = sortedCellMassive[q];
                    q++;
                }
            }
        }
    }

    // ������ ���������
    private void DoCorrup()
    {
        m_x = Mathf.RoundToInt(hero.transform.position.x);
        m_y = Mathf.RoundToInt(hero.transform.position.y);

        if (cellHandlers != null)
        {
            // cellHandlers[m_y, m_x].CorruptionLevelUp(corrupForce);
            if (corrupForce >= 0) cellHandlers[m_y, m_x].CorruptionLevelUp(corrupForce);
            if (corrupForce < 0) cellHandlers[m_y, m_x].CorruptionLevelDown(-corrupForce);
        }
    }

    //������� ����� ���������� ������
    IEnumerator CorupTimeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            DoCorrup();
        }
    }

    //�������� ��������
    private void OnDestroy()
    {
        StopCoroutine("CorupTimeCycle");
    }
}
