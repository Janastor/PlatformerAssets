using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class HealthText : HealthDisplay
{
    private TMP_Text _text;
    private const string DisplayFormat = "0";
    private const char Slash = '/';
    private Coroutine _changeHealthBarValueCoroutine;
    
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = _currentHealth.ToString(DisplayFormat) + Slash + _maxHealth.ToString(DisplayFormat);
    }
    
    protected override void ChangeValue()
    {
        _text.text = _currentHealth.ToString(DisplayFormat) + Slash + _maxHealth.ToString(DisplayFormat);
    }
}
