using Code.Core;
using Code.Core.BuildMode;
using Code.Enums;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPurchaseButton : MonoBehaviour
{
    [SerializeField] private int levelAfterWhichAvailable;
    [SerializeField] private GameObject towerPrefab; 
    [SerializeField] private int towerCost = 25; 
    [SerializeField] private TextMeshProUGUI costText;
    
    private Button _button;
    private bool _blockedByGameMode;

    private void Awake()
    {
        costText.text = towerCost.ToString();
        
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);

        CoreEventsProvider.LevelSwitched += ChangeAvailability;
        CoreEventsProvider.BalanceChanged += UpdatePurchaseAbility;
        
        GameModeManager.GameModeChanged += OnChangeGameMode;
    }

    private void OnButtonClick()
    {
        var spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
        var tower = ObjectsManager.CreateObject(towerPrefab, spawnPosition);
        tower.GetComponent<TowerBuildHandler>().TowerCost = towerCost;
        
        CoreManager.Instance.BalanceMediator.Substract(towerCost);
        GameModeManager.SetBuildMode();
        DestroyObjectsHandler.Add(tower);
    }

    private void UpdatePurchaseAbility(int currentBalance)
    {
        if (!_blockedByGameMode)
            ChangeInteractAbility(currentBalance >= towerCost);
    }

    private void OnChangeGameMode(GameMode gameMode)
    {
        var canInteract = gameMode is GameMode.Default;
        if (!canInteract)
        {
            ChangeInteractAbility(false);
            _blockedByGameMode = true;
            return;
        }

        _blockedByGameMode = false;
        UpdatePurchaseAbility(CoreManager.Instance.BalanceMediator.CurrentBalance);
    }
    
    private void ChangeInteractAbility(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    private void ChangeAvailability(int levelIndex)
    {
        gameObject.SetActive(levelIndex >= levelAfterWhichAvailable);
    }
    
}