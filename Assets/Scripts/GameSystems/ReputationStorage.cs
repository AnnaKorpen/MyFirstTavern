using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationStorage : MonoBehaviour
{
    // Holds all information about reputation:
    // - current level,
    // - current amount of reputation, it is added by customers after thay get order and leave
    // - reputation needed for the next level according to LevelInfo
    public static ReputationStorage Instance { get; private set; }
    public event EventHandler OnAddReputation;
    public event EventHandler OnSetParameters;

    private int playerReputation = 0;
    private int reputationAddPerCustomer = 10;
    private int currentLevel;
    private int ruputationNeededToNextLevel;

    private void Awake()
    {
        Instance = this;
    }

    public void AddReputation()
    {
        playerReputation += reputationAddPerCustomer;
        OnAddReputation?.Invoke(this, EventArgs.Empty);
    }

    public int GetReputation()
    {
        return playerReputation;
    }

    public int GetReputationLevel()
    {
        return currentLevel;
    }

    public int GetReputationToNextLevel()
    {
        return ruputationNeededToNextLevel;
    }

    public void LoadReputationFromSaveFile(int reputation)
    {
        playerReputation = reputation;
    }

    public void SetReputationParametres(int reputationLevel, int reputationToNextLevel)
    {
        currentLevel = reputationLevel;
        ruputationNeededToNextLevel = reputationToNextLevel;
        OnSetParameters?.Invoke(this, EventArgs.Empty);
    }
}
