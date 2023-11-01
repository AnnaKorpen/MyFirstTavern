using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Hold the desicion to load the game, TavernGameManager read this decision in the GameScene
    public static GameStarter Instance { get; private set; }

    private bool isLoadingGame = false;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsLoadingGame()
    { 
        return isLoadingGame; 
    }

    public void SetLoadingGame(bool isLoadingGame)
    { 
        // The desicion to load the game is set by pressing load button in MainMenuUI
        this.isLoadingGame = isLoadingGame;
    }
}
