using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using UnityEngine;

public class KitchenObjectProducerVisual : MonoBehaviour
{
    // Visualise producing and collecting products

    [SerializeField] KitchenObjectProducer kitchenObjectProducer;
    [SerializeField] Transform[] kitchenObjectProducerVisuals;
    [SerializeField] private Transform productContainer;
    [SerializeField] private Transform productStartPosition;

    private float productSpacing = 0.5f;
    private int productsPerRow = 7;

    private void Awake()
    {
        Instantiate(kitchenObjectProducerVisuals[0], transform);
    }

    private void Start()
    {
        kitchenObjectProducer.OnChangeCurrentProductionList += KitchenObjectProducer_OnChangeCurrentProductionList;
    }


    private void KitchenObjectProducer_OnChangeCurrentProductionList(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // Remove all current prefabs and creates new prefabs acoording to products list
        foreach (Transform child in productContainer)
        {
            if (child == productStartPosition) continue;
            Destroy(child.gameObject);

        }

        for (int i = 0; i < kitchenObjectProducer.GetCurrentProductionList().Count; i++)
        {
            Transform kictenObjectSOTransform = Instantiate(kitchenObjectProducer.GetCurrentProductionList()[i].prefab, productContainer);
            int kitchenObjectRow = i / productsPerRow;
            int kitchenObjectColumn = i - (kitchenObjectRow * productsPerRow);
            Vector3 kictenObjectSOTransformPosition = new Vector3(productStartPosition.position.x + productSpacing * kitchenObjectColumn, productStartPosition.position.y, productStartPosition.position.z - productSpacing * kitchenObjectRow);
            kictenObjectSOTransform.position = kictenObjectSOTransformPosition;
        }

    }
}
