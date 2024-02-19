using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class TemporaryHealthDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TMP_Text _text;
    private string _displayText = "Health: ";
    
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _player.TookDamage += SetDisplay;
        _player.Healed += SetDisplay;
        SetDisplay();
    }
    
    private void OnDestroy()
    {
        _player.TookDamage -= SetDisplay;
        _player.Healed -= SetDisplay;
    }

    private void SetDisplay()
    {
        _text.text = _displayText + _player.CurrentHealth;
    }
}
