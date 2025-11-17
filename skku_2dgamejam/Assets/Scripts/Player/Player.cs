using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerManualMove _playerManualMove;
    public float Speed = 3f;

    public GameObject AttackPrefab; 
    public float AttackRange = 1f;
    public int damage = 10;

    public LayerMask enemyLayer;   // 적 레이어

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

        private void Attack()
        {
            // 1) 공격 이미지 생성
            GameObject slash = Instantiate(AttackPrefab, transform.position, Quaternion.identity);

            // 0.2초 후 자동 삭제
            Destroy(slash, 0.2f);

            // 2) 공격 범위 안에 있는 적 찾기
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, AttackRange, enemyLayer);

            foreach (Collider2D enemy in enemies)
            {
                Enemy minienemy = enemy.GetComponent<Enemy>();
                if (minienemy != null)
                {
                    minienemy.Hit(damage);
                }
            }
        }

 }

