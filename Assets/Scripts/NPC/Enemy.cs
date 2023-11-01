using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy moves from position to position if it is not hited by the Player
    // After three hit enemy dyes

    public event EventHandler OnHit; 
    public event EventHandler OnDying;

    private Vector3 zeroPosition;
    private Vector3 distance = new Vector3(-3, 0, 0);
    private Vector3 targetPosition;
    private float speed = 2f;
    private float idleTimer;
    private float idleTimeMax = 5f;
    private float hittingTimer;
    private float hittingTimeMax = 2f;
    private bool arrivedToDestination = true;
    private bool canBeingHit = true;
    private int damage = 0;
    private bool isDying = false;

    private void Start()
    {
        zeroPosition = transform.position;
        transform.LookAt(zeroPosition + distance);
    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
        if(transform.position == targetPosition) 
        { 
            arrivedToDestination = true;
        }
    }

    void Update()
    {
        if(arrivedToDestination)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > idleTimeMax)
            {
                idleTimer = 0;
                arrivedToDestination = false;
                if (transform.position == zeroPosition + distance)
                {
                    targetPosition = zeroPosition; 
                }
                else
                {
                    targetPosition = zeroPosition + distance;
                }
            }
        }
        if(!arrivedToDestination && damage == 0 && !isDying) 
        {
            MoveTo(targetPosition);
        }
        if(damage > 0)
        {
            arrivedToDestination = true;
            hittingTimer += Time.deltaTime;
            if(hittingTimer > hittingTimeMax)
            {
                hittingTimer = 0;
                canBeingHit = true;
            }
        }

    }

    public void TryHit()
    {
        if(canBeingHit)
        {
            // Doew nor allow to be hited every update
            damage += 1;
            OnHit?.Invoke(this, EventArgs.Empty);
            canBeingHit = false;
            Debug.Log(damage);
            if (damage > 2)
            {
                Die();
            }
        }
    }

    public bool IsWalking()
    {
        if(arrivedToDestination)
        { 
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Die()
    {
        isDying = true;
        OnDying?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject, 1.5f);
    }
}
