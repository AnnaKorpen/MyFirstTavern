using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    // Class initiates rooms acoording to the LevelInfo set by TavernGameManager
    public static RoomSystem Instance { get; private set; }

    [SerializeField] private Transform mainRoomPrefab;
    [SerializeField] private Transform storageRoomPrefab;
    [SerializeField] private Transform storageWallPrefab;

    private List<Transform> wallsList;
    private List<Transform> roomsList = new List<Transform>();


    private void Awake()
    {
        Instance = this;
        wallsList = new List<Transform>();
        // Main room should be initiated in the begining of the game
        Transform newRoom = Instantiate(mainRoomPrefab, transform);
        newRoom.position = transform.position;
        roomsList.Add(newRoom);
    }

    public void SetRoomNumber(int roomNumber)
    { 
        if (roomNumber == 2 && roomsList.Count < 2)
        {
            // Destory previous walls and creates a storage room
            if (wallsList.Count > 0)
            { 
                Destroy(wallsList[0].gameObject);
                wallsList.RemoveAt(0);
            }

            Transform newRoom = Instantiate(storageRoomPrefab, transform);
            newRoom.position = transform.position;
            roomsList.Add(newRoom);
        }
        if (roomNumber == 1)
        {
            // Creats walli in place, where the second room would be
            if (wallsList.Count == 0)
            {
                Transform wall = Instantiate(storageWallPrefab, transform);
                wall.position = transform.position;
                wallsList.Add(wall);
            }
        }
    }

}
