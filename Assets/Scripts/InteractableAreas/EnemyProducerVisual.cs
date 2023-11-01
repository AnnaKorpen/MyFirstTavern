using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProducerVisual : MonoBehaviour
{
    // Visualise producing meat by creating meat prefab
    // If Player takes maet, than it destroys the prefab
    private EnemyProducer enemyProducer;

    private void Start()
    {
        enemyProducer = GetComponent<EnemyProducer>();
        enemyProducer.OnProductionListChanged += EnemyProducer_OnProductionListChanged;
    }

    private void EnemyProducer_OnProductionListChanged(object sender, System.EventArgs e)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in enemyProducer.GetKitchenObjectSOList())
        {
            Transform productTransform = Instantiate(kitchenObjectSO.prefab, transform);
            productTransform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }
}
