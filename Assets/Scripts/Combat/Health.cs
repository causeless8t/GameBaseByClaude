using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHp = 30;

    private int _currentHp;

    public event Action OnDamaged;
    public event Action OnDied;

    public int CurrentHp => _currentHp;
    public int MaxHp => _maxHp;

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHp <= 0)
        {
            return;
        }

        _currentHp -= damage;

        OnDamaged?.Invoke();

        if (_currentHp <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public void HealToFull()
    {
        _currentHp = _maxHp;
    }
}
