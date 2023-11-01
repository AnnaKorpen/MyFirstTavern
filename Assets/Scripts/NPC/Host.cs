using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : MonoBehaviour
{
    // Host interactes with CounterDesk instead of the Player every specific amount of time
    public event EventHandler OnInteracting;

    private CounterDeskArea hostCounterDeskArea;
    private float adjustingTimer;
    private float adjustingTimeMax = 5f;
    
    void Start()
    {
        EmployeeSystem.Instance.SetEmployee(0);

        if (EmployeeSystem.Instance.GetHostPlaceOfWork().gameObject.TryGetComponent<CounterDeskArea>(out CounterDeskArea counterDeskArea))
        {
            hostCounterDeskArea = counterDeskArea;
            hostCounterDeskArea.OnGettingTableForCustomer += HostCounterDeskArea_OnGettingTableForCustomer;
        }
            
    }

    private void HostCounterDeskArea_OnGettingTableForCustomer(object sender, System.EventArgs e)
    {
        OnInteracting?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {
        adjustingTimer += Time.deltaTime;
        if(adjustingTimer > adjustingTimeMax)
        {
            adjustingTimer = 0;
            if(hostCounterDeskArea != null)
            {
                hostCounterDeskArea.GetInteraction();
            }
        }
        
    }
}
