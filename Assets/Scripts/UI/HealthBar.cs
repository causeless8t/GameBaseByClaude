using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private bool _startVisible;

    [SerializeField]
    private SpriteRenderer _background;

    [SerializeField]
    private SpriteRenderer _fill;

    private Health _health;
    private bool _visible;
    private Vector3 _fillOriginalScale;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
        _fillOriginalScale = _fill.transform.localScale;

        SetVisible(_startVisible);
    }

    private void Update()
    {
        if (!_visible && _health.CurrentHp < _health.MaxHp)
        {
            SetVisible(true);
        }

        UpdateFill();
    }

    private void SetVisible(bool visible)
    {
        _visible = visible;
        _background.enabled = visible;
        _fill.enabled = visible;
    }

    private void UpdateFill()
    {
        var ratio = Mathf.Clamp01(_health.CurrentHp / (float)_health.MaxHp);

        var scale = _fillOriginalScale;
        scale.x = _fillOriginalScale.x * ratio;
        _fill.transform.localScale = scale;
    }
}
