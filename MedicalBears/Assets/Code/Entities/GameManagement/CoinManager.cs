using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text coinText; 
    private int coinBalance = 0; 

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coinBalance += amount;
        UpdateCoinText();
    }

    public void SubtractCoins(int amount)
    {
        coinBalance -= amount;
        if (coinBalance < 0)
        {
            coinBalance = 0; 
        }
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = "Монеты: " + coinBalance;
    }

    public int GetCoinBalance()
    {
        return coinBalance;
    }
}