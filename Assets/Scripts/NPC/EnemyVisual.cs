using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    // Visualise enemy's movements and how he is dying
    [SerializeField] Enemy enemy;

    private const string IS_WALKING = "IsWalking";
    private const string IS_HIT = "IsHit";
    private const string IS_DYING = "IsDying";
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        enemy.OnHit += Enemy_OnHit;
        enemy.OnDying += Enemy_OnDying;
    }

    private void Enemy_OnDying(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_DYING);
    }

    private void Enemy_OnHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_HIT);
        audioSource.Play();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, enemy.IsWalking());
    }
}
