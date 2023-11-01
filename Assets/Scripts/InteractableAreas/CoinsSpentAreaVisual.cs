using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsSpentAreaVisual : MonoBehaviour
{
    // Visualise information about the price and the money spent on CoinSpentArea
    [SerializeField] private CoinSpentArea coinSpentArea;
    [SerializeField] private TextMeshProUGUI coinSpentText;

    private void Update()
    {
        coinSpentText.text = "������� \r\n" + coinSpentArea.GetPlayerMoneySpentOn().ToString() + " $\r\n�� " + coinSpentArea.GetObjectPrice().ToString() + " $";
    }

}
