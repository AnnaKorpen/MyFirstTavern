using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    // Allows to save the game before going to MainMenuScene
    [SerializeField] private Button saveButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SaveSystem saveSystem;

    private void Awake()
    {

        saveButton.onClick.AddListener(() =>
        {
            saveSystem.SaveGame();
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
}
