using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernGameManager : MonoBehaviour
{
    //Class regulates game loading and new level achieving
    public static TavernGameManager Instance { get; private set; }
    public event EventHandler OnNewLevel;
    public event EventHandler OnGameEnd;

    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private Transform startGamePlayerPosition;

    private int currentLevel = 1;
    private LevelReader.LevelInfo currentLevelInfo;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (GameStarter.Instance.IsLoadingGame())
        {
            // if GameStarter holds the decision to load the game, the game is loading
            saveSystem.LoadGame();
            LevelReader.Instance.ReadLevelsInfo();
            currentLevelInfo = LevelReader.Instance.LoadCurrentLevelInfo(currentLevel);
            SetCurrentLevelInfo();
        }
        else
        {
            // if not, TavernGameManager start a new game from first level
            currentLevel = 1;
            LevelReader.Instance.ReadLevelsInfo();
            currentLevelInfo = LevelReader.Instance.LoadCurrentLevelInfo(currentLevel);
            SetCurrentLevelInfo();
            PlayerCharacter.Instance.gameObject.transform.position = startGamePlayerPosition.position;
        }

        ReputationStorage.Instance.OnAddReputation += ReputationStorage_OnAddReputation;
    }

    private void SetCurrentLevelInfo()
    {
        // After reading LevelInfo from txt file, info load to proper instances
        PlayerCharacter.Instance.SetCurrentLevelInfo(currentLevelInfo.playerSpeed, currentLevelInfo.playerCapacity);
        RoomSystem.Instance.SetRoomNumber(currentLevelInfo.roomNumber);
        EmployeeSystem.Instance.SetEmployeeNumber(currentLevelInfo.employeeNumber, currentLevelInfo.producerPrice);
        WaitingTablesSystem.Instance.SetWaitingTableNumber(currentLevelInfo.waitingTablesNumber, currentLevelInfo.dichesInMenu);
        ProducerSystem.Instance.SetProducerNumber(currentLevelInfo.producerNumber, currentLevelInfo.producerPrice);
        ReputationStorage.Instance.SetReputationParametres(currentLevelInfo.level, currentLevelInfo.reputationNeededForNextLevel);
        MoneyStorage.Instance.SetTipsValue(currentLevelInfo.tipsIncreaser);
    }

    private void ReputationStorage_OnAddReputation(object sender, System.EventArgs e)
    {
        // Check if there is enough reputation to achieve a new level 
        if (ReputationStorage.Instance.GetReputation() == currentLevelInfo.reputationNeededForNextLevel)
        {
            if (currentLevel < 10)
            {
                Time.timeScale = 0f;
                currentLevel += 1;
                currentLevelInfo = LevelReader.Instance.LoadCurrentLevelInfo(currentLevel);
                SetCurrentLevelInfo();
                OnNewLevel?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // If it is a final level, the game ends
                Time.timeScale = 0f;
                OnGameEnd?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    public void LoadLeveNumber(int levelNumber)
    {
        currentLevel = levelNumber;
    }

    public int GetLevelNumber()
    {
        return currentLevel;
    }
    public string GetLevelWarning()
    {
        return currentLevelInfo.newLevelWarning;
    }

}
