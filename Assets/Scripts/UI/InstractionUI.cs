using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstractionUI : MonoBehaviour
{
    // Shows instructions and start the game after resume is pressed
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        });
    }
}
