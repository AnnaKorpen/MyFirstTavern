using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TipsCreator : MonoBehaviour
{
    // Class creates interactable objects - coins - near the waiting table after it changes state to waiting cleaning

    [SerializeField] private WaitingTable waitingTable;
    [SerializeField] private Transform coinsPrefab;
    
    private int coinsNumber;
   

    private void Start()
    {
        waitingTable.OnChangingState += WaitingTable_OnChangingState;
    }

    private void WaitingTable_OnChangingState(object sender, System.EventArgs e)
    {
        if(waitingTable.GetState() == WaitingTable.State.WaitingCleaning)
        {
            CreateTips();
        }
    }

    private void CreateTips()
    {
        // Number of coins depends on the amount of tips in current LevelInfo
        coinsNumber = MoneyStorage.Instance.GetTipsValue() / MoneyStorage.Instance.GetCoinsValue();
        float coinsSpacing = 0.4f;

        for (int i = 0; i < coinsNumber; i++)
        {
            Transform coins = Instantiate(coinsPrefab, transform);
            Vector3 coinsPosition = new Vector3(transform.position.x + coinsSpacing * i, transform.position.y, transform.position.z);
            coins.position = coinsPosition;
        }
    }

}
