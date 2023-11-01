using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUI : MonoBehaviour
{
    //Shows reputation level and its progress

    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private Image progressBarFiller;

    private void Start()
    {
        ReputationStorage.Instance.OnAddReputation += ReputationStorage_OnAddReputation;
        ReputationStorage.Instance.OnSetParameters += RaputationStorage_OnSetParameters;
    }

    private void RaputationStorage_OnSetParameters(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void ReputationStorage_OnAddReputation(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        levelNumberText.text = "Уровень " + ReputationStorage.Instance.GetReputationLevel().ToString();
        int currentReputation = ReputationStorage.Instance.GetReputation();
        int neededReputation = ReputationStorage.Instance.GetReputationToNextLevel();
        progressBarFiller.fillAmount = (float) currentReputation / (float) neededReputation;


    }
}
