using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpentArea : MonoBehaviour, IInteractableArea
{
    // Class is an interactable object, it takes Player's money to buy a new producer or an employee
    // Class is initiated by ProducerSystem and EmployeeSystem

    private Transform objectPlayerBuyTransform;
    private Vector3 objectFuturePosition;
    private int playerMoneySpentOn;
    private int objectPrice;

    public void SetParameters(int price, Transform objectPlayerBuy, Vector3 objectPosition)
    {
        // Get paramaters to create a proper CoinSpentArea
        objectPrice = price;
        objectPlayerBuyTransform = objectPlayerBuy;
        objectFuturePosition = objectPosition;
    }

    public void GetInteraction()
    {
        int standardMoneyDeduction = MoneyStorage.Instance.GetStandardMoneyDeduction();
        int playerMoney = MoneyStorage.Instance.GetPlayerMoney();

        if (playerMoney > standardMoneyDeduction && playerMoneySpentOn < objectPrice)
        {
            // Check if there is enough Player's money to take them and holds all money that the Player spent on Area
            MoneyStorage.Instance.SubtractPlayerMoney(standardMoneyDeduction);
            playerMoneySpentOn += standardMoneyDeduction;
            MoneyStorage.Instance.AddMoneyInCoinSpentArea(standardMoneyDeduction);
            playerMoney = MoneyStorage.Instance.GetPlayerMoney();
        }
        if(playerMoneySpentOn == objectPrice)
        {
            // If Player spent enouth money to byu an object, creates the object
            MoneyStorage.Instance.SubtractMoneyInCoinSpentAreay(objectPrice);
            Transform objectPlayerBuy = Instantiate(objectPlayerBuyTransform);
            objectPlayerBuy.position = objectFuturePosition;
            Destroy(gameObject);
        }
    }

    public int GetObjectPrice()
    {
        return objectPrice;

    }

    public int GetPlayerMoneySpentOn()
    {
        return playerMoneySpentOn;
    }

}
