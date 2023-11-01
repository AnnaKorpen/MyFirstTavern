using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectProducer : MonoBehaviour, IInteractableArea
{
    // Class produce a product from ingredients, the recipe is set in Inspector 
    // If there is a product, it tries give it to Player

    public event EventHandler OnChangeCurrentProductionList;

    [SerializeField] private List<RecipeSO> possibleRecipesList;
    [SerializeField] private Transform producingArea;
   
    private int productionListMax = 6;

    private List<KitchenObjectSO> currentIngredientsList;
    private List<KitchenObjectSO> currentProductionList;
    private float recipeTimer;

    private void Awake()
    {
        currentIngredientsList = new List<KitchenObjectSO>();
        currentProductionList = new List<KitchenObjectSO>();
    }

    private void Start()
    {
        ProducerSystem.Instance.SetProducer();
    }

    public void GetInteraction()
    {

        if (PlayerCharacter.Instance.GetChildKitchenObjectSOList().Count > 0)
        {
            // Player has something

            KitchenObjectSO possibleIngredient = IsPossibleIngredient(PlayerCharacter.Instance.GetChildKitchenObjectSOList());
            if (possibleIngredient != null)
            {
                // It is possible ingredient, add it as an ingredient

                currentIngredientsList.Add(possibleIngredient);
                PlayerCharacter.Instance.RemoveChildKitchenObjectSO(possibleIngredient);
            }
            else
            {
                // It is not a possible ingredient, try to give kitchenObjectSO from the currentProductionList to the Player

                TryGivePlayerKitchenObjectSO();
            }
        }
        else
        {
            // Player is empty, try to give kitchenObjectSO from the currentProductionList to the Player

            TryGivePlayerKitchenObjectSO();

        }
    }

    public List<KitchenObjectSO> GetCurrentProductionList()
    {
        return currentProductionList;
    }


    private KitchenObjectSO IsPossibleIngredient(List<KitchenObjectSO> kitchenObjectSOList)
    {
        // Check if Producer has Recipe that uses ingredients that Player holds
        KitchenObjectSO possibleIngredient = null;

        foreach (RecipeSO possibleRecipe in possibleRecipesList)
        {
            if(possibleRecipe.recipeIngredient != null)
            {
                foreach(KitchenObjectSO kitchenObjectSO in kitchenObjectSOList)
                {
                    if(possibleRecipe.recipeIngredient.objectName == kitchenObjectSO.objectName)
                    {
                        possibleIngredient = possibleRecipe.recipeIngredient;
                        break;
                    }
                }
            }
        }

        return possibleIngredient;
        
    }

    private void Update()
    {
        if(currentProductionList.Count < productionListMax)
        {
            // Produer can produce another kitchenObjectSO
            if (currentIngredientsList.Count > 0)
            {
                //Producer has ingredients

                foreach (RecipeSO possibleRecipeSO in possibleRecipesList)
                {
                    if (currentIngredientsList[0] == possibleRecipeSO.recipeIngredient)
                    {
                        ProduceKitchenObjectSO(possibleRecipeSO, true);
                        break;
                    }
                }
            }
            else
            {
                // Producer does not have ingredient
                foreach (RecipeSO possibleRecipeSO in possibleRecipesList)
                {
                    if (possibleRecipeSO.recipeIngredient == null)
                    {
                        // There is a possible recipe, which produce kitchenObjectSO without ingredients
                        ProduceKitchenObjectSO(possibleRecipeSO, false);
                        break;
                    }
                }
            }
        }        
        
    }

    private void ProduceKitchenObjectSO(RecipeSO possibleRecipeSO, bool fromIngredient)
    {
        recipeTimer += Time.deltaTime;
        if (recipeTimer >= possibleRecipeSO.recipeWaitingTime)
        {
            recipeTimer = 0;
            KitchenObjectSO newProduct = Instantiate(possibleRecipeSO.recipeResult, producingArea);
            currentProductionList.Add(newProduct);
            if(fromIngredient)
            {
                currentIngredientsList.RemoveAt(0);
            }
            OnChangeCurrentProductionList?.Invoke(this, EventArgs.Empty);
        }
    }

    private void TryGivePlayerKitchenObjectSO()
    {
        if (currentProductionList.Count > 0)
        {
            // Product has been produced, try to give it to Player
            if(PlayerCharacter.Instance.TryAddChildKitchenObjectSO(currentProductionList[0]))
            {
                currentProductionList.RemoveAt(0);
                OnChangeCurrentProductionList?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
