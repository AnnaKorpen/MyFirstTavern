using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Class Customer is regulated by other class, which send it to proper locations:
    // QueueSystem sends it to CounerDesk
    // CounterDesk sends it to the table
    // Table make new order and tells that to customer
    // Table send customer to leave 

    public event EventHandler OnStartWalking;
    public event EventHandler OnStopWalking;
    public event EventHandler OnSittingDown;
    public event EventHandler OnStandingUp;

    [SerializeField] private LayerMask stopMovementLayerMask;

    private float customerSpeed = 2f;
    private Vector3 targetPosition;
    private Vector3 targetToLook;
    private bool arrivedToDestination = false;
    private bool isLeaving = false;

    public void MoveTo(Vector3 position)
    {
        // Move to position and look at it
        targetPosition = position;
        targetToLook = position;
        arrivedToDestination = false;
    }

    public void MoveToAndLookAT(Vector3 position, Vector3 positionToLook)
    {
        // Move to postition and look at another
        targetPosition = position;
        targetToLook = positionToLook;
        arrivedToDestination = false;
    }

    private void Update()
    {
        HandleMovement();

        if (isLeaving && arrivedToDestination)
        {
            // Destory customer when it left
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        float customerRadius = .7f;
        float moveDistance = customerSpeed * Time.deltaTime;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, customerRadius, transform.forward, moveDistance, stopMovementLayerMask);
        if (canMove)
        {
            // There is no player in the way
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, customerSpeed * Time.deltaTime);
            transform.LookAt(targetToLook);
            if (transform.position == targetPosition)
            {
                OnStopWalking?.Invoke(this, EventArgs.Empty);
                arrivedToDestination = true;
            }
            else
            {
                OnStartWalking?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool IsArrived()
    {
        return arrivedToDestination;
    }

    public void MakeOrder()
    {
        OnSittingDown?.Invoke(this, EventArgs.Empty);
    }

    public void Leave()
    {
        // Leave to the door and creates new reputation for the Player
        OnStandingUp?.Invoke(this, EventArgs.Empty);
        MoveTo(QueueSystem.Instance.GetCustomerIstantiatePosition());
        isLeaving = true;
        ReputationStorage.Instance.AddReputation();
    }


}
