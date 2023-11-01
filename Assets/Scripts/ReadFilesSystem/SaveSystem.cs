using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static LevelReader;

public class SaveSystem: MonoBehaviour
{
    // Creates a special class to form a string to SaveFile
    // If game is loading, create a special class from a SaveFile string and pass it to proper game systems
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int playerMoney;
        public int playerReputation;
        public Vector3 playerPosition;
        public int producersInAction;
        public List<bool> employees;
    }

    private SaveData saveData;

    public void SaveGame()
    {
        saveData = new SaveData();
        saveData.level = TavernGameManager.Instance.GetLevelNumber();
        saveData.playerMoney = MoneyStorage.Instance.GetPlayerMoney() + MoneyStorage.Instance.GetMoneyInCoinSpentAreay();
        saveData.playerReputation = ReputationStorage.Instance.GetReputation();
        saveData.playerPosition = PlayerCharacter.Instance.gameObject.transform.position;
        saveData.producersInAction = ProducerSystem.Instance.GetProducersInAction();
        saveData.employees = EmployeeSystem.Instance.GetEmployeesList();
        string saveDataString = JsonUtility.ToJson(saveData);
        ReadFileSystem.CreateSaveFile(saveDataString);

    }

    public void LoadGame()
    {
        string saveDataString = ReadFileSystem.LoadSaveFile();
        if (saveDataString != null)
        {
            saveData = JsonUtility.FromJson<SaveData>(saveDataString);
            SetLoadData();
        }

    }

    private void SetLoadData()
    {
        TavernGameManager.Instance.LoadLeveNumber(saveData.level);
        MoneyStorage.Instance.AddPlayerMoney(saveData.playerMoney);
        PlayerCharacter.Instance.gameObject.transform.position = saveData.playerPosition;
        ReputationStorage.Instance.LoadReputationFromSaveFile(saveData.playerReputation);
        if (saveData.employees[0])
        {
            EmployeeSystem.Instance.InstantiateHost();
        }
        if (saveData.employees[1])
        {
            EmployeeSystem.Instance.InstatiateMagicCleaner();
        }
        ProducerSystem.Instance.LoadProducersFromSaveFile(saveData.producersInAction);
    }

}
