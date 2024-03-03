using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class CoinCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int _coinCount = 0;
    private string _displayText = "Coins: ";

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void AddCoin()
    {
        _coinCount++;
        _text.text = _displayText + _coinCount;
    }
}
