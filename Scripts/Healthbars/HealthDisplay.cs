using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour
{
    [SerializeField] protected EntityHealth _entityHealth;
    
    protected float _currentHealth => _entityHealth.Health;
    protected float _maxHealth => _entityHealth.MaxHealth;
    
    private void OnEnable()
    {
        _entityHealth.HealthChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _entityHealth.HealthChanged -= ChangeValue;
    }

    protected abstract void ChangeValue();
}
