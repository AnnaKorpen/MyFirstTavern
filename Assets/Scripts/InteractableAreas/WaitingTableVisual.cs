using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTableVisual : MonoBehaviour
{
    // Visualise that table has an order, custoner is eating or table needs to be clean
    [SerializeField] private WaitingTable waitingTable;
    [SerializeField] private Transform waitingOrderIconHolder;
    [SerializeField] private Transform plateHolder;
    [SerializeField] private Transform dirtyDishesVisual;
    [SerializeField] private Transform eatingDishesVisual;

    private void Start()
    {
        waitingTable.OnChangingState += WaitnigTable_OnChangingState;
    }

    private void WaitnigTable_OnChangingState(object sender, System.EventArgs e)
    {
        if ( waitingTable.GetState() == WaitingTable.State.MakingOrder)
        {
            // Creat a proper prefab for the order
            Transform waitingOrderIcon = Instantiate(waitingTable.GetWaitingOrder().prefab, waitingOrderIconHolder);
            waitingOrderIcon.position = waitingOrderIconHolder.position;
        }
        if (waitingTable.GetState() == WaitingTable.State.HaveCustomer)
        {
            // Destory prefab for the order and visualise that customer is eating
            foreach (Transform child in waitingOrderIconHolder)
            {
                Destroy(child.gameObject);
            }
            Transform eatingDiches = Instantiate(eatingDishesVisual, plateHolder);
            eatingDiches.position = plateHolder.position;
        }
        if (waitingTable.GetState() == WaitingTable.State.WaitingCleaning)
        {
            // Visualise that table is dirty
            foreach (Transform child in plateHolder)
            {
                Destroy(child.gameObject);
            }
            Transform dirtyDiches = Instantiate(dirtyDishesVisual, plateHolder);
            dirtyDiches.position = plateHolder.position;
        }
        if (waitingTable.GetState() == WaitingTable.State.WaitingCustomer)
        {
            // Visualise that table is clean and ready for a new customer
            foreach (Transform child in plateHolder)
            {
                Destroy(child.gameObject);
            }

        }
    }
}
