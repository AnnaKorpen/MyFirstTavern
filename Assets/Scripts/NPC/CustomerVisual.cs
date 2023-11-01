using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    // Visualise customer movements
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private const string IS_SITTING = "IsSitting";
    [SerializeField] private Customer customer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        customer.OnStartWalking += Customer_OnStartWalking;
        customer.OnStopWalking += Customer_OnStopWalking;
        customer.OnSittingDown += Customer_OnSittingDown;
        customer.OnStandingUp += Customer_OnStandingUp;

    }

    private void Customer_OnStandingUp(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_SITTING, false);
    }

    private void Customer_OnSittingDown(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_SITTING, true);
    }

    private void Customer_OnStopWalking(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_WALKING, false);
    }

    private void Customer_OnStartWalking(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_WALKING, true);
    }

}
