using NUnit.Framework.Internal.Commands;
using UnityEngine;

public enum EItemType
{
    AttackPower,
    AttackSpeedUp,
}

public class Item : MonoBehaviour
{
    [Header("아이템 타입")]
    public EItemType Type;
    public float Value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;

        Apply(other);
        Destroy(gameObject);

    }

    private void Apply(Collider2D other)
    {
        switch (Type)
        {
            case EItemType.AttackPower:
                {
                    PlayerManualMove playerManualMove = other.GetComponent<PlayerManualMove>();
                    break;
                }
            case EItemType.AttackSpeedUp:
                {
                    PlayerManualMove playerManualMove = other.GetComponent<PlayerManualMove>();
                    break;
                }
        }
    }
}
