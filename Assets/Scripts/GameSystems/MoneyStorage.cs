using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStorage : MonoBehaviour
{
    //Class contains all information about game money:
    //- add and subtract Player money,
    //- hold information about money at areas, where Player can spend money (save them if the Player wants to exit the game),
    // - know the coins value and tips and pass it to TipsCreator and Coins

    public static MoneyStorage Instance { get; private set; }
    private int standardTipsValue = 100;
    private int tipsValue = 100;
    private int playerMoney = 0;
    private int moneyInCoinSpentArea = 0;
    private int coinsValue = 100;
    private int standardMoneyDeduction = 10;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPlayerMoney(int money)
    {
        playerMoney += money;
    }

    public void SubtractPlayerMoney(int money)
    {
        playerMoney -= money;
    }

    public void AddMoneyInCoinSpentArea(int money)
    {
        moneyInCoinSpentArea += money;
    }

    public void SubtractMoneyInCoinSpentAreay(int money)
    {
        moneyInCoinSpentArea -= money;
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    public int GetMoneyInCoinSpentAreay()
    {
        return moneyInCoinSpentArea;
    }

    public int GetCoinsValue()
    { 
        return coinsValue; 
    }

    public int GetStandardMoneyDeduction()
    { 
        return standardMoneyDeduction;
    }

    public void SetTipsValue(int tipsIncreaser)
    {
        tipsValue = standardTipsValue * tipsIncreaser;
    }

    public int GetTipsValue()
    {
        return tipsValue;
    }

}
