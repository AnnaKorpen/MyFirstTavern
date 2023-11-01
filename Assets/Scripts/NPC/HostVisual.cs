using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostVisual : MonoBehaviour
{
    // Visualise host interactions with customers
    private Animator animator;
    [SerializeField] private Host host;
    private const string IS_INTERACTING = "IsInteracting";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        host.OnInteracting += Host_OnInteracting;
    }

    private void Host_OnInteracting(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_INTERACTING);
    }
}
