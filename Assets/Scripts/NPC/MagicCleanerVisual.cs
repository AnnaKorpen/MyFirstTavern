using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCleanerVisual : MonoBehaviour
{
    // Visualise MagicCleaner movements and cleaning
    [SerializeField] MagicCleaner magicCleaner;
    private const string IS_CLEANING = "IsCleaning";
    private Animator animator;

    private void Start()
    {
        magicCleaner.OnStartCleaning += MagicCleaner_OnStartCleaning;
        animator = GetComponent<Animator>();    
    }

    private void MagicCleaner_OnStartCleaning(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_CLEANING);
    }
}
