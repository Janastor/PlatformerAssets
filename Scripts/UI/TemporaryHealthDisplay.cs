using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class TemporaryHealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    private TMP_Text _text;
    private string _displayText = "Health: ";
    
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _playerHealth.HealthChanged += SetDisplay;
        SetDisplay();
    }
    
    private void OnDestroy()
    {
        _playerHealth.HealthChanged -= SetDisplay;
    }

    private void SetDisplay()
    {
        _text.text = _displayText + _playerHealth.Health;
    }
}
