using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class CoinCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int _coinCount = 0;
    private string _displayText = "Coins: ";

    public void AddCoin()
    {
        _coinCount++;
        _text.text = _displayText + _coinCount;
    }

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
}
