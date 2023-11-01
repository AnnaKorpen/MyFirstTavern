using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelUI : MonoBehaviour
{
    // Shows that new level is acheived and what progress the Player has
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private Text levelwarningText;
    [SerializeField] private Button resumeButton;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        resumeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);

        });
    }
    public void UpdateVisual()
    {
        levelNumberText.text = "Уровень " + TavernGameManager.Instance.GetLevelNumber().ToString();
        levelwarningText.text = TavernGameManager.Instance.GetLevelWarning();
        audioSource.Play();

    }

}