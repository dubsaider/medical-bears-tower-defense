using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager; 
    [SerializeField] private GameObject towerPrefab; 
    [SerializeField] private int towerCost = 25; 
    [SerializeField] private Button button; 
    [SerializeField] private Color insufficientBalanceColor = Color.gray;
    [SerializeField] private Color sufficientBalanceColor = Color.white; 

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        UpdateButtonColor();
    }
    void Update()
    {
        UpdateButtonColor();
    }

    void OnButtonClick()
    {
        int currentBalance = coinManager.GetCoinBalance();

        if (currentBalance >= towerCost)
        {
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
            Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
            coinManager.SubtractCoins(towerCost);
            UpdateButtonColor();
        }
    }

    void UpdateButtonColor()
    {
        int currentBalance = coinManager.GetCoinBalance();

        if (currentBalance >= towerCost)
        {
            button.image.color = sufficientBalanceColor;
        }
        else
        {
            button.image.color = insufficientBalanceColor;
        }
    }
}