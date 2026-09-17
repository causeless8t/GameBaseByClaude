using UnityEngine;

public class EnemyPlayerReference : MonoBehaviour
{
    public PlayerProgression PlayerProgression { get; private set; }
    public Transform PlayerTransform { get; private set; }

    private void Awake()
    {
        PlayerProgression = FindFirstObjectByType<PlayerProgression>();

        if (PlayerProgression == null)
        {
            Debug.LogError("EnemyPlayerReference: PlayerProgression을 찾을 수 없습니다.", this);
            return;
        }

        PlayerTransform = PlayerProgression.transform;
    }
}
