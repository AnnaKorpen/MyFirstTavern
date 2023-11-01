using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReader : MonoBehaviour
{
    // Creates a special class from a InfoFile string that TavernGameManager uses to pass proper LevelInfo
    [System.Serializable]
    public class LevelInfo
    {
        public int level;
        public int reputationNeededForNextLevel;
        public int roomNumber;
        public int employeeNumber;
        public float playerSpeed;
        public int playerCapacity;
        public int waitingTablesNumber;
        public int dichesInMenu;
        public int producerNumber;
        public int producerPrice;
        public int tipsIncreaser;
        public string newLevelWarning;
    }

    [System.Serializable]
    public class LevelsInfoList
    {
        public LevelInfo[] levelsInfoList;
    }

    public static LevelReader Instance { get; private set; }

    private LevelsInfoList currentLevelsInfoList = new LevelsInfoList();

    private void Awake()
    {
        Instance = this;
    }
    public void ReadLevelsInfo()
    {
        string levelsInfo = ReadFileSystem.LoadLevelsInfoFile();
        if (levelsInfo != null)
        {
            currentLevelsInfoList = JsonUtility.FromJson<LevelsInfoList>(levelsInfo);
        }
    }

    public LevelInfo LoadCurrentLevelInfo(int currentLevel)
    {
        LevelInfo currentLevelInfo = null;

        foreach(LevelInfo levelInfo in currentLevelsInfoList.levelsInfoList)
        {
            if (levelInfo.level == currentLevel)
            {
                currentLevelInfo = levelInfo;
            }
        }

        return currentLevelInfo;
    }

}
