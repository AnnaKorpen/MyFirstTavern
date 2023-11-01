using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProducer : MonoBehaviour, IInteractableArea
{
    // Every specific amount of time creates a new enemy and if enemy dies, creates a new product - meat
    // Contains information about productes that has been created, if Player interactes - gives a product to the Player

    public event EventHandler OnProductionListChanged;

    [SerializeField] Transform enemyTransform;
    [SerializeField] KitchenObjectSO product;

    private Enemy currentEnemy;
    private List<KitchenObjectSO> currentProductionList = new List<KitchenObjectSO>();
    private float enemySpawnTimer;
    private float enemySpawnTimeMax = 5f;


    private void InstantiateEnemy()
    {
        Transform newEnemyTransform = Instantiate(enemyTransform, transform);
        newEnemyTransform.position = transform.position;
        Enemy newEnemy = newEnemyTransform.gameObject.GetComponent<Enemy>();
        currentEnemy = newEnemy;
        newEnemy.OnDying += NewEnemy_OnDying;
        
    }

    private void CreateNewProduct()
    {
        currentProductionList.Add(product);
        OnProductionListChanged?.Invoke(this, EventArgs.Empty);
    }

    private void NewEnemy_OnDying(object sender, System.EventArgs e)
    {
        CreateNewProduct();
        currentEnemy.OnDying -= NewEnemy_OnDying;
        currentEnemy = null;
    }

    private void Update()
    {
        if (currentEnemy == null && WaitingTablesSystem.Instance.GetDishesInMenuNumber() > 5) 
        {
            enemySpawnTimer += Time.deltaTime;
            if (enemySpawnTimer > enemySpawnTimeMax)
            {
                enemySpawnTimer = 0;
                InstantiateEnemy();
            }
        }
    }

    public void GetInteraction()
    {

        if (currentProductionList.Count > 0)
        {
            // Product has been produced, try to give it to Player
            if (PlayerCharacter.Instance.TryAddChildKitchenObjectSO(currentProductionList[0]))
            {
                currentProductionList.RemoveAt(0);
                OnProductionListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return currentProductionList;
    }
}
