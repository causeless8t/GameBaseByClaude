using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Health))]
public class PlayerProgression : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    private int _expToNextLevel = 10;

    [SerializeField]
    [Min(0)]
    private int _attackPowerGrowth = 2;

    private int _level;
    private int _currentExp;

    private PlayerAttack _playerAttack;
    private Health _health;

    public int Level => _level;
    public int CurrentExp => _currentExp;
    public int ExpToNextLevel => _expToNextLevel;

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _health = GetComponent<Health>();
        _level = 1;
        _currentExp = 0;
    }

    public void AddExp(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _currentExp += amount;

        while (_currentExp >= _expToNextLevel)
        {
            _currentExp -= _expToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _level++;
        _playerAttack.IncreaseAttackPower(_attackPowerGrowth);
        _health.HealToFull();
    }
}
