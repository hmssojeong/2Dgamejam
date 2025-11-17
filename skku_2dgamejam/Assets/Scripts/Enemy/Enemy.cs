using UnityEngine;
using UnityEngine.UIElements;

public enum EEnemyType
{
    Directional,
    Trace,
    Fly
}
public class Enemy : MonoBehaviour
{
    public float TopMax = 4f;
    public float BottomMax = 2f;
    private float _currentPosition;
    private float direction = 3.0f;

    [Header("스탯")]
    public float Speed = 1.0f;
    public float _health = 100f;

    [Header("적 타입")]
    public EEnemyType Type;

    [Header("아이템 프리팹")]
    public GameObject ItemPrefabs;

    private void Start()
    {
        _currentPosition = transform.position.x;
    }



    public void Update()
    {
        if(Type == EEnemyType.Directional)
        {
            MoveDirectional();
        }
        else if(Type == EEnemyType.Trace)
        {
            MoveTrace();
        }
        else if(Type == EEnemyType.Fly)
        {
            MoveFly();
        }
    }

    private void MoveDirectional()
    {
        Vector2 direction = Vector2.left;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void MoveTrace()
    {
        // 1. 플레이어의 위치를 구한다.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null) return;
        Vector2 playerPosition = playerObject.transform.position;

        // 2. 위치에 따라 방향을 구한다.
        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction.Normalize();

        // 3. 방향에 맞게 이동한다.
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void MoveFly()
    {
        _currentPosition += Time.deltaTime * direction;
        if(_currentPosition >= TopMax)
        {
            direction *= -1; //이동속도+방향에 -1로 반전
            _currentPosition = TopMax;
        }
        else if(_currentPosition <= BottomMax)
        {
            direction *= -1;
            _currentPosition = BottomMax;
        }

        transform.position = new Vector3(0, _currentPosition, 0);
    }



    public void Hit(float damage)
    {
        _health -= damage;
        if(_health <= 0 )
        {
            Death();
        }
    }

    private void Death()
    {
       /* DropItem();*/
    }

/*    private void DropItem()
    {
        if(Random.Range(0,2) == 0)
        {
            Item.Instance.AttackPower;
        }

        else
        {
            Item.Instance.AttackSpeed;
        }
    }*/

}