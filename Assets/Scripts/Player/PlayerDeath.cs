using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerDeath : MonoBehaviour
{
    private Health _health;
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerInput = GetComponent<PlayerInput>();
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
        _playerMovement.enabled = false;
        _playerAttack.enabled = false;
        _playerInput.enabled = false;
    }
}
