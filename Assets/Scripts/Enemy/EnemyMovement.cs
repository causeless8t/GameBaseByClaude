using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyPlayerReference))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private float _detectionRange = 4f;

    [SerializeField]
    private float _stopDistance = 1f;

    private Rigidbody2D _rigidbody2D;
    private EnemyPlayerReference _playerReference;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerReference = GetComponent<EnemyPlayerReference>();
    }

    private void FixedUpdate()
    {
        var target = _playerReference.PlayerTransform;

        if (target == null)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }

        var toTarget = target.position - transform.position;
        var distance = toTarget.magnitude;

        if (distance <= _detectionRange && distance > _stopDistance)
        {
            _rigidbody2D.linearVelocity = toTarget.normalized * _moveSpeed;
        }
        else
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stopDistance);
    }
}
