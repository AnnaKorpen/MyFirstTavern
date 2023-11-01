using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTable : MonoBehaviour, IInteractableArea        
{
    // Class is interectable object, it makes orders, check if they correct, tells customers to leave
    // Every table has several states, Player can interact if there is an order or table needs to be clean
    public event EventHandler OnChangingState;
    public enum State
    {
        WaitingCustomer,
        MakingOrder,
        WaitingOrder,
        HaveCustomer,
        WaitingCleaning,
    }
    
    [SerializeField] private List<RecipeSO> menuList;
    [SerializeField] private Transform customerPosition;

    private KitchenObjectSO waitingOrder;
    private State state;
    private Customer currentCustomer;
    private float havingCustomerTimer;
    private float havingCustomerTimeMax = 5f;


    private void Start()
    {
        state = State.WaitingCustomer;
    }

    private void Update()
    {
        switch(state) 
        { 
            case State.WaitingCustomer:
                // CounterDesk may send a new customer to the table
                break;
            case State.MakingOrder:
                if(currentCustomer != null && currentCustomer.IsArrived())
                {
                    // Creates a new order
                    MakeNewOrder();
                    currentCustomer.MakeOrder();
                    state = State.WaitingOrder;
                }
                break;
            case State.WaitingOrder:
                // Player can give order to the table
                break;
            case State.HaveCustomer:
                // Spend some time to feel like customer is eating
                havingCustomerTimer += Time.deltaTime;
                if(havingCustomerTimer > havingCustomerTimeMax)
                {
                    havingCustomerTimer = 0;
                    currentCustomer.Leave();
                    state = State.WaitingCleaning;
                    OnChangingState?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.WaitingCleaning:
                // Player or a MagicClean can clean the table, e new customer can arrive only if table is clean
                break;
        }
    }

    private KitchenObjectSO IsRightOrder()
    {
        // Check if Player hols the right order

        KitchenObjectSO rightOrder = null;

        foreach(KitchenObjectSO kitchenObjectSO in PlayerCharacter.Instance.GetChildKitchenObjectSOList())
        {
            if(kitchenObjectSO.objectName == waitingOrder.objectName)
            {
                rightOrder = kitchenObjectSO;
                break;
            }
        }

        return rightOrder;
    }

    private void MakeNewOrder()
    {
        // Randomly choose order from the list according to current LevelInfo 
        waitingOrder = menuList[UnityEngine.Random.Range(0, WaitingTablesSystem.Instance.GetDishesInMenuNumber())].recipeResult;
        OnChangingState?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObjectSO GetWaitingOrder()
    {
        return waitingOrder;
    }

    public State GetState() 
    { 
        return state; 
    }

    public void SetState(State state)
    { 
        this.state = state; 
    }


    public void GetInteraction()
    {
        if (state == State.WaitingOrder)
        {
            if (PlayerCharacter.Instance.GetChildKitchenObjectSOList().Count > 0)
            {
                // Player has something
                KitchenObjectSO rightOrder = IsRightOrder();

                if (rightOrder != null)
                {
                    // It is right order
                    PlayerCharacter.Instance.RemoveChildKitchenObjectSO(rightOrder);
                    state = State.HaveCustomer;
                    OnChangingState?.Invoke(this, EventArgs.Empty);
                }

            }
        }
        if (state == State.WaitingCleaning)
        {
            state = State.WaitingCustomer;
            OnChangingState?.Invoke(this, EventArgs.Empty);
        }

    }

    public Vector3 GetCustomerPosition()
    {
        return customerPosition.position;
    }

    public void SetCustomer(Customer customer)
    {
        currentCustomer = customer;
    }
}
