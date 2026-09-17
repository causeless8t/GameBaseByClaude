using UnityEngine;

[RequireComponent(typeof(EnemyPlayerReference))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int _attackPower = 5;

    [SerializeField]
    private float _attackRange = 1f;

    [SerializeField]
    private float _attackInterval = 1f;

    private EnemyPlayerReference _playerReference;
    private float _nextAttackTime;

    private void Awake()
    {
        _playerReference = GetComponent<EnemyPlayerReference>();
    }

    private void Update()
    {
        var target = _playerReference.PlayerTransform;

        if (target == null || Time.time < _nextAttackTime)
        {
            return;
        }

        var distance = Vector2.Distance(transform.position, target.position);
        if (distance > _attackRange)
        {
            return;
        }

        var health = target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(_attackPower);
        }

        _nextAttackTime = Time.time + _attackInterval;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
