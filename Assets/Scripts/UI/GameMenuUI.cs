using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    // Starts all UIs in GameScene, change them when nessecary 
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject gameMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject newLevelUI;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject instructionUI;

    private void Awake()
    {
        gameMenuUI.SetActive(true);
        instructionUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        newLevelUI.SetActive(false);
        endGameUI.SetActive(false);
        Time.timeScale = 0f;

        exitButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
        });
    }

    private void Start()
    {
        TavernGameManager.Instance.OnNewLevel += TavernManager_OnNewLevel;
        TavernGameManager.Instance.OnGameEnd += TavernGameManager_OnGameEnd;
    }

    private void TavernGameManager_OnGameEnd(object sender, System.EventArgs e)
    {
        // If max reputation level is achieved
        gameMenuUI.SetActive(false);
        endGameUI.SetActive(true);
    }

    private void TavernManager_OnNewLevel(object sender, System.EventArgs e)
    {
        // If a new level is achieved
        newLevelUI.SetActive(true);
        newLevelUI.gameObject.GetComponent<NewLevelUI>().UpdateVisual();
    }

}
