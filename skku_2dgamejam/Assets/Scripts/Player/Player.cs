using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerManualMove _playerManualMove;
    public float Speed = 3f;

    public GameObject AttackPrefab; 
    public float AttackRange = 1f;
    public float AttackSpeed = 1f;
    public float Damage = 10;

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
                    if(collider.tag == "Enemy")
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}

