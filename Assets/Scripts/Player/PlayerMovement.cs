using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        _moveInput = Vector2.ClampMagnitude(value.Get<Vector2>(), 1f);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _moveInput * _moveSpeed;
    }
}
