using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorageVisual : MonoBehaviour
{
    // Visualise that Player is holding something 
    [SerializeField] private Transform PlayerStorageStarter;

    private float productSpacing = 0.5f;
    private int productsPerRow = 3;

    private void Start()
    {
        PlayerCharacter.Instance.OnChildKitchenObjectSOListChanged += PlayerCharacter_OnChildKitchenObjectSOListChanged;

    }

    private void PlayerCharacter_OnChildKitchenObjectSOListChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // Destroys all prefabs and makes new prefabs after Player's child list has been changed
        foreach (Transform child in transform)
        {
            if (child == PlayerStorageStarter) continue;
            Destroy(child.gameObject);

        }

        for (int i = 0; i < PlayerCharacter.Instance.GetChildKitchenObjectSOList().Count; i++)
        {
            Transform kictenObjectSOTransform = Instantiate(PlayerCharacter.Instance.GetChildKitchenObjectSOList()[i].prefab, transform);
            int kitchenObjectColumn = ((i + 1) % productsPerRow) - 1;
            int kitchenObjectRow = i / productsPerRow;
            if(kitchenObjectRow > 1)
            {
                kitchenObjectRow = -1;
            }
            Vector3 kictenObjectSOTransformPosition = new Vector3(transform.position.x + productSpacing * kitchenObjectColumn, transform.position.y, transform.position.z + productSpacing * kitchenObjectRow);
            kictenObjectSOTransform.position = kictenObjectSOTransformPosition;
        }

    }
}
