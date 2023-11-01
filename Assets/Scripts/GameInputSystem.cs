using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInputSystem : MonoBehaviour
{
    // Class regulates all inputs
    public static GameInputSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
    }
    public Vector2 GetMovementVectorNormalized()
    {
        // Return new position for the Player
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        inputVector = inputVector.normalized;
        return inputVector;

    }

    private void Update()
    {
        // Clear all that Player is holding

        if (Input.GetKey(KeyCode.Space))
        {
            PlayerCharacter.Instance.RemoveAllChildKitchenObjectSO();
        }
    }

}
