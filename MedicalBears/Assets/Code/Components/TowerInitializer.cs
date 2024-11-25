using UnityEngine;
using Code.Enums;

public class TowerInitializer : MonoBehaviour
{
    public GameObject arrowPrefab;

    void Start()
    {
        Tower tower = GetComponent<Tower>();

        switch (tower.TowerType)
        {
            case TowerType.LaserTower:
                tower.Initialize(TowerType.LaserTower, new LaserAttackComponent());
                break;

            case TowerType.XRayTower:
                tower.Initialize(TowerType.XRayTower, new XRayAttackComponent());
                break;

            case TowerType.BottleTower:
                tower.Initialize(TowerType.BottleTower, new BottleAttackComponent());
                break;

            case TowerType.TestTower:
                tower.Initialize(TowerType.TestTower, new ArrowAttackComponent(arrowPrefab));
                break;
        }
    }
}