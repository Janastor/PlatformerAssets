using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _valueChangeSpeed;
    
    private Coroutine _changeHealthBarValueCoroutine;
    
    private void Start()
    {
        _healthbar.value = _normalizedHealth;
        
    }

    protected override void ChangeValue()
    {
        if (_changeHealthBarValueCoroutine != null)
            StopCoroutine(_changeHealthBarValueCoroutine);

        _changeHealthBarValueCoroutine = StartCoroutine(SmoothChangeValueCoroutine());
    }

    private IEnumerator SmoothChangeValueCoroutine()
    {
        while (_healthbar.value != _normalizedHealth)
        {
            _healthbar.value = Mathf.MoveTowards(_healthbar.value, _normalizedHealth, _valueChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
