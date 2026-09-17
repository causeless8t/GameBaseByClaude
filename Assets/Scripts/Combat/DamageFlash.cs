using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class DamageFlash : MonoBehaviour
{
    [SerializeField]
    private Color _flashColor = Color.red;

    [SerializeField]
    private float _flashDuration = 0.1f;

    private Health _health;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        _health.OnDamaged -= HandleDamaged;

        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            _flashCoroutine = null;
        }

        _spriteRenderer.color = _originalColor;
    }

    private void HandleDamaged()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
        }

        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.color = _flashColor;

        yield return new WaitForSeconds(_flashDuration);

        _spriteRenderer.color = _originalColor;
        _flashCoroutine = null;
    }
}
