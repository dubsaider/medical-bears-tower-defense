using Code.Core;
using Code.Core.BuildMode;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab; 
    [SerializeField] private int towerCost = 25; 
    [SerializeField] private Button button; 
    [SerializeField] private Color insufficientBalanceColor = Color.gray;
    [SerializeField] private Color sufficientBalanceColor = Color.white; 

    void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
        CoreEventsProvider.BalanceChanged += UpdateButtonColor;
    }


    void OnButtonClick()
    {
        //TODO вынести эту проверку в отдельный BuyTowerHandler или подобный
        if (CoreManager.Instance.BalanceMediator.CurrentBalance >= towerCost)
        {
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
            var tower = ObjectsManager.CreateObject(towerPrefab, spawnPosition);
            
            DestroyObjectsHandler.Add(tower);
            
            CoreManager.Instance.BalanceMediator.Substract(towerCost);
        }
    }

    void UpdateButtonColor(int currentBalance)
    {
        button.image.color = currentBalance >= towerCost 
            ? sufficientBalanceColor 
            : insufficientBalanceColor;
    }
}