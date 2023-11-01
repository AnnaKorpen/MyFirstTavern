using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerParametesUI : MonoBehaviour
{
    // Shows Playr's money, speed and capacity
    [SerializeField] TextMeshProUGUI playerSpeedText;
    [SerializeField] TextMeshProUGUI playerStrengthText;
    [SerializeField] TextMeshProUGUI playerMoneyText;

    private void Start()
    {
        PlayerCharacter.Instance.OnParametersSet += Player_OnParametersSet;
    }

    private void Player_OnParametersSet(object sender, PlayerCharacter.OnParametersSetArgs e)
    {
        playerSpeedText.text = e.speed.ToString();
        playerStrengthText.text = e.strength.ToString();
    }

    private void Update()
    {
        playerMoneyText.text = MoneyStorage.Instance.GetPlayerMoney().ToString();
    }
}
