using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerManualMove _playerManualMove;
    public float Speed = 3f;
    private float _health = 200;

    public GameObject AttackPrefab;
    public float AttackRange = 1f;
    public float AttackSpeed = 1f;
    public float Damage = 20;

    private Animator _animator;


    private float curTime;
    public float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        

    }
    private void Update()
    {


        if (Input.GetMouseButtonDown(0) && curTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<Enemy>().Hit(10);
                    }
                }

            }
            _animator.SetTrigger("Attack1");
            curTime = coolTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
    public void Hit(float Damage)
    {
        _health -= Damage;
        if(_health <=0)
        {
            Destroy(gameObject);
            ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
            scoreManager.BestScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터는 플레이어와만 충돌처리할 것이다.
        if (!other.gameObject.CompareTag("Enemy")) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.Hit(Damage);

    }
}

