using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyPlayerReference))]
public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private int _expReward = 5;

    private Health _health;
    private EnemyPlayerReference _playerReference;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerReference = GetComponent<EnemyPlayerReference>();
    }

    private void OnEnable()
    {
        _health.OnDied += Die;
    }

    private void OnDisable()
    {
        _health.OnDied -= Die;
    }

    private void Die()
    {
        if (_playerReference.PlayerProgression != null)
        {
            _playerReference.PlayerProgression.AddExp(_expReward);
        }

        Destroy(gameObject);
    }
}
