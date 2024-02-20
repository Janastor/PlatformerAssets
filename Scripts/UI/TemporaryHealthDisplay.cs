using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class TemporaryHealthDisplay : MonoBehaviour
{
    [SerializeField] private EntityHealth entityHealth;

    private TMP_Text _text;
    private string _displayText = "Health: ";
    
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        entityHealth.HealthChanged += SetDisplay;
        SetDisplay();
    }
    
    private void OnDestroy()
    {
        entityHealth.HealthChanged -= SetDisplay;
    }

    private void SetDisplay()
    {
        _text.text = _displayText + entityHealth.Health;
    }
}
