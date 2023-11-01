using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterVisual : MonoBehaviour
{
    // Visualise that Player is moving and hitting an enemy
    [SerializeField] GameObject staff;

    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private const string HIT_ENEMY = "hitEnemy";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerCharacter.Instance.OnHitEnemy += Player_OnHitEnemy;
        staff.SetActive(false);
    }

    private void Player_OnHitEnemy(object sender, System.EventArgs e)
    {
        animator.SetTrigger(HIT_ENEMY);
        staff.SetActive(true);
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, PlayerCharacter.Instance.IsWalking());
        if(PlayerCharacter.Instance.IsWalking())
        {
            staff.SetActive(false);
        }

    }
}
