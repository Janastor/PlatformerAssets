using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : HealthDisplay
{
    protected Slider _healthbar;
    
    protected float _normalizedHealth => _currentHealth / _maxHealth;
    
    protected void Awake()
    {
        _healthbar = GetComponent<Slider>();
    }
    
    protected override void SetValue()
    {
        _healthbar.value = _normalizedHealth;
    }

    protected override void ChangeValue()
    {
        _healthbar.value = _normalizedHealth;
    }
}
