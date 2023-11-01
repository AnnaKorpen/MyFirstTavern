using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    //Class for every product in the game
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
