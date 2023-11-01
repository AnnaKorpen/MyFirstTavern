using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCleaner : MonoBehaviour
{
    // Cleans tables if they need cleaning instead of the Player

    public event EventHandler OnStartCleaning;

    private bool isCleaning = false;
    private WaitingTable waitingTableToClean;
    private float cleaningTimer;
    private float cleaningTimeMax = 3f;
    private Vector3 zeroPosition;

    private void Start()
    {
        EmployeeSystem.Instance.SetEmployee(1);
    }

    private void LookForTableToClean()
    {
        List<WaitingTable> waitingtableList = WaitingTablesSystem.Instance.GetWaitingTablesList();
        foreach (WaitingTable waitingtable in waitingtableList)
        {
            if (waitingtable.GetState() == WaitingTable.State.WaitingCleaning)
            {
                waitingTableToClean = waitingtable;
                zeroPosition = transform.position;
                OnStartCleaning?.Invoke(this, EventArgs.Empty);
                isCleaning = true;
                break;
            }
        }
    }

    private void StartAutoCleaning()
    {
        cleaningTimer += Time.deltaTime;
        Vector3 sideStep = new Vector3(0, 1.5f, 0);
        transform.position = waitingTableToClean.transform.position + sideStep;

        if (cleaningTimer > cleaningTimeMax )
        {
            cleaningTimer = 0;
            waitingTableToClean.GetInteraction();
            transform.position = zeroPosition;
            isCleaning = false;
        }

    }    

    private void Update()
    {
        if (!isCleaning)
        {
            LookForTableToClean();
        }
        else
        {
            StartAutoCleaning();
        }

    }


}
