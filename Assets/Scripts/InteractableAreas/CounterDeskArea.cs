using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterDeskArea : MonoBehaviour, IInteractableArea
{
    // Every specific amount of time allows Player or  Host to send the first Customer in queue to the table
    public event EventHandler OnGettingTableForCustomer;

    private float interactionTimer;
    private float interactionTimeMax = 3f;
    private bool canInteract = false;


    public void GetInteraction()
    {
        if(canInteract)
        {
            GetTableForCustomer();
        }
    }

    private void GetTableForCustomer()
    {
        //Try to find a table waiting for a customer
        List<WaitingTable> waitingTablesList = WaitingTablesSystem.Instance.GetWaitingTablesList();
        foreach (WaitingTable waitingTable in waitingTablesList)
        {
            if (waitingTable.GetState() == WaitingTable.State.WaitingCustomer)
            {
                //There is a table waiting for a customer, try to get first customer in Queue
                Customer firstCustomer = QueueSystem.Instance.GetFirstCustomer();
                if (firstCustomer != null)
                {
                    OnGettingTableForCustomer?.Invoke(this, EventArgs.Empty);
                    waitingTable.SetCustomer(firstCustomer);
                    waitingTable.SetState(WaitingTable.State.MakingOrder);
                    firstCustomer.MoveToAndLookAT(waitingTable.GetCustomerPosition(), waitingTable.gameObject.transform.position);
                    canInteract = false;
                    break;
                }
            }
        }
    }

    private void Update()
    {
        if (!canInteract)
        {
            interactionTimer += Time.deltaTime;
            if (interactionTimer > interactionTimeMax)
            {
                interactionTimer = 0f;
                canInteract = true;
            }
        }
    }
}
