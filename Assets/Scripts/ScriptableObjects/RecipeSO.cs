using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    //Class for every Recipe in the game
    public KitchenObjectSO recipeIngredient;
    public KitchenObjectSO recipeResult;
    public string recipeName;
    public float recipeWaitingTime;

}
