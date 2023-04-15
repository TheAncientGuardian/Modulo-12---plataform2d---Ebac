using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TextMeshProUGUI uiTextCoins; 
    private void Start() 
    {
        Reset();
    }

    private void Reset() 
    {
        coins.Value = 0;
        UpdateUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins.Value += amount;
        UpdateUI();
    }

    private void UpdateUI() 
    {
        uiTextCoins.text = coins.ToString();
    }
}
