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

    private SpriteRenderer spriter;
    private Rigidbody2D rigid;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        spriter = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
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
                        collider.GetComponent<Enemy>().Hit(50);
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
             OnDamaged(transform.position);
        }

        private void OnDamaged(Vector2 targetPos)
        {
            gameObject.layer = 6;
            spriter.color = new Color(1, 0, 0, 0.4f);
   

        int direction = transform.position.x - targetPos.x > 0 ? 1 : -1;

        Vector2 jumpVelocity = Vector2.up * 4f;
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
            //좌우로 튕김
            rigid.AddForce(new Vector2(direction, 1)*5, ForceMode2D.Impulse);
        
            //3초뒤에 "OffDamaged"가 실행된다.
            Invoke("OffDamaged", 3);
         }
    void OffDamaged()
    {
        //스프라이트의 색상과 투명도를 원래대로
        spriter.color = new Color(1, 1, 1, 1);
        //플레이어의 레이어를 원래대로
        gameObject.layer = 9;
    }
}

