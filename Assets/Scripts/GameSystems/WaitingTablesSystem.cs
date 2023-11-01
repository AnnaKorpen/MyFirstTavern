using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WaitingTablesSystem : MonoBehaviour
{
    // Holds information about all the tables in the scene, creates new table according to LevelInfo
    // and holds information about dishes that can be ordered in the current level
    public static WaitingTablesSystem Instance { get; private set; }

    [SerializeField] private Transform waitingTablePrefab;
    [SerializeField] private Transform waitingTableStartPosition;

    private List<WaitingTable> waitingTablesList;
    private int dishesInMenu;
    private int previousWaitingTableNumber = 0;

    private void Awake()
    {
        Instance = this;
        waitingTablesList = new List<WaitingTable>();
    }


    public void SetWaitingTableNumber(int waitingTableNumber, int dishesNumber)
    {
        // Check if a new table is needed according to the LevelInfo 
        if(waitingTableNumber > previousWaitingTableNumber)
        {
            // Initiate a new table
            InstantiateWaitingTable(waitingTableNumber - previousWaitingTableNumber);
            previousWaitingTableNumber = waitingTableNumber;
        }
        dishesInMenu = dishesNumber;
    }

    private void InstantiateWaitingTable(int tableNumber)
    {
        int tablesInRow = 4;
        float tableSpacingInColumn = 5f;
        float tableSpacingInRow = 6f;

        // Position of the tables depends on the table number and the first table position

        for (int i = 0; i < tableNumber; i++)
        {
            Transform waitingTableTransform = Instantiate(waitingTablePrefab, transform);
            int tableRow = (previousWaitingTableNumber + i) / tablesInRow;
            int tableColumn = (previousWaitingTableNumber + i) - (tableRow * tablesInRow);
            Vector3 tablePosition = new Vector3(waitingTableStartPosition.position.x + tableColumn * tableSpacingInColumn, waitingTableStartPosition.position.y, waitingTableStartPosition.position.z - tableSpacingInRow * tableRow);
            waitingTableTransform.position = tablePosition;
            waitingTablesList.Add(waitingTableTransform.gameObject.GetComponent<WaitingTable>());
        }
    }
   

    public List<WaitingTable> GetWaitingTablesList()
    {
        return waitingTablesList;
    }

    public int GetDishesInMenuNumber()
    {
        // Return information about dishes that can be ordered in the current level
        return dishesInMenu;
    }
}
