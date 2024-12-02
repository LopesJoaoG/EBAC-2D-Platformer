using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.Singleton;

public class ItemManager : MonoBehaviour
{
    public TextMeshProUGUI UICoins;
    public int coins;
    public static ItemManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Reset();
    }
    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateInterfaceUI();
    }

    private void UpdateInterfaceUI()
    {
        UICoins.text = "x " + coins.ToString();
    }
}
