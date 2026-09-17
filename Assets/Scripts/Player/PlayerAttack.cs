using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private int _attackPower = 10;

    [SerializeField]
    private float _attackRange = 1f;

    [SerializeField]
    private LayerMask _enemyLayerMask;

    public int AttackPower => _attackPower;

    private void OnAttack(InputValue value)
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, _attackRange, _enemyLayerMask);

        foreach (var hit in hits)
        {
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(_attackPower);
            }
        }
    }

    public void IncreaseAttackPower(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _attackPower += amount;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
