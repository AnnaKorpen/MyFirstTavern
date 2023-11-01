using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour, IInteractableArea
{
    // Class is an interactable object, it collects coins after Player has interacted with it

    private int coinsValue;
    private float lifeTimeAfterHit = 0.1f;
    private bool isCollected = false;
    private AudioSource audioSource;

    private void Start()
    {
        coinsValue = MoneyStorage.Instance.GetCoinsValue();
        audioSource = GetComponent<AudioSource>();
    }

    public void GetInteraction()
    {
        if(!isCollected)
        {
            // Allows only one interaction with Coins
            isCollected = true;
            MoneyStorage.Instance.AddPlayerMoney(coinsValue);
            audioSource.Play();
            Destroy(gameObject, lifeTimeAfterHit);
        }
    }
}
