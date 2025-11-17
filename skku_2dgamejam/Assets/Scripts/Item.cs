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

    public Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }
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
                    AttackPower();
                    break;
                }
            case EItemType.AttackSpeedUp:
                {
                    AttackSpeedUp();
                    break;
                }
        }
    }

    private void AttackPower()
    {
        float AttackPower = player.Damage + 10f;
    }

    private void AttackSpeedUp()
    {
        float AttackSpeed = player.AttackSpeed + 2f;
    }

    }
