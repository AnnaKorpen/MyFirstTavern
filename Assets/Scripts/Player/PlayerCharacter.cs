using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacter : MonoBehaviour
{
    // Holds Player movements, hitting enemies and interacting with objects
    public static PlayerCharacter Instance { get; private set; }
    public event EventHandler OnChildKitchenObjectSOListChanged;
    public event EventHandler OnHitEnemy;
    public event EventHandler<OnParametersSetArgs> OnParametersSet;
    public class OnParametersSetArgs: EventArgs
    {
        public float speed;
        public int strength;
    }

    [SerializeField] private GameInputSystem gameInputSystem;
    [SerializeField] private LayerMask stopMovementLayerMask;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Transform kitchenObjectHolder;

    private float moveSpeed = 7f;
    private bool isWalking;
    private bool canCollectKitchenObjectSO;
    private float timeForCollectingKitchenObjectSO;
    private float timeForCollectingKitchenObjectSOMax = 1f;
    private float playerRadius = .7f;
    private float playerHeight = 2f;
    private List<KitchenObjectSO> childKitchenObjectSOList;
    private int childKitchenSOListMax = 9;

    private void Awake()
    {
        Instance = this;
        childKitchenObjectSOList = new List<KitchenObjectSO>();
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
        HandleBattle();
        HandleCollectingKitchenObjectSO();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInputSystem.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance, stopMovementLayerMask);
        if (!canMove)
        {
            // Attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance, stopMovementLayerMask);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Attempt only z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance, stopMovementLayerMask);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void HandleInteraction()
    {
        float interactionDistance = playerHeight;

        if (Physics.Raycast(transform.position, transform.position + Vector3.down, out RaycastHit raycastHit, interactionDistance, interactionLayerMask))
        {
            // Player hit something from interactionLayerMask
            if (raycastHit.collider.gameObject.TryGetComponent<IInteractableArea>(out IInteractableArea raycastHitInteractableArea))
            {
                // It is an interactable area
                raycastHitInteractableArea.GetInteraction();
            }
        }

    }

    private void HandleBattle()
    {
        float interactionDistance = playerHeight;

        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, transform.forward, out RaycastHit raycastHit, interactionDistance, enemyLayerMask))
        {
            //Player hits enemy
            raycastHit.collider.gameObject.GetComponent<Enemy>().TryHit();
            OnHitEnemy?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleCollectingKitchenObjectSO()
    {
        // Allows to collect objects only one at a time
        if(!canCollectKitchenObjectSO)
        {
            timeForCollectingKitchenObjectSO += Time.deltaTime;
            if(timeForCollectingKitchenObjectSO > timeForCollectingKitchenObjectSOMax)
            {
                timeForCollectingKitchenObjectSO = 0;
                canCollectKitchenObjectSO = true;
            }
        }

    }

    public List<KitchenObjectSO> GetChildKitchenObjectSOList()
    {
            return childKitchenObjectSOList;
    }

    public bool TryAddChildKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        // Check if there is enough place to hold an object (that Player's capacity allows it)
        if (childKitchenObjectSOList.Count < childKitchenSOListMax && canCollectKitchenObjectSO)
        {
            childKitchenObjectSOList.Add(kitchenObjectSO);
            OnChildKitchenObjectSOListChanged?.Invoke(this, EventArgs.Empty);
            canCollectKitchenObjectSO = false;
            return true;
        }
        return false;
    }

    public void RemoveChildKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO childKitchenObjectSO in childKitchenObjectSOList)
        {
            if(childKitchenObjectSO.objectName == kitchenObjectSO.objectName)
            {
                childKitchenObjectSOList.Remove(childKitchenObjectSO);
                OnChildKitchenObjectSOListChanged?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }

    public void RemoveAllChildKitchenObjectSO()
    {
        childKitchenObjectSOList.Clear();
        OnChildKitchenObjectSOListChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void SetCurrentLevelInfo(float speed, int capacity)
    {
        // Takes Player's parameters according to current LevelInfo
        moveSpeed = speed;
        childKitchenSOListMax = capacity;
        OnParametersSet?.Invoke(this, new OnParametersSetArgs { 
            speed = speed, 
            strength = capacity});
    }
}
