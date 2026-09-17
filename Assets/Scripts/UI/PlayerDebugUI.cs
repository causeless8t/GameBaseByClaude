using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerDebugUI : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private PlayerProgression _playerProgression;

    [SerializeField]
    private PlayerAttack _playerAttack;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text =
            $"LV. {_playerProgression.Level}\n" +
            $"HP {_health.CurrentHp} / {_health.MaxHp}\n" +
            $"EXP {_playerProgression.CurrentExp} / {_playerProgression.ExpToNextLevel}\n" +
            $"ATK {_playerAttack.AttackPower}";
    }
}
