using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManualMove : MonoBehaviour
{
    public float ShiftSpeed = 1.2f;
    public float Speed = 1f;
    
    [Header("시작위치")]
    private Vector2 _originPosition;

    [Header("이동범위")]
    public float MinX = -7f;
    public float MaxX = 7.4f;
    public float MinY = -4;
    public float MaxY = 4;

    private Player _player;

    private Animator _animator;

    [SerializeField]
    private float _bounce = 5f; //점프 높이
    private Rigidbody2D _rigid2D;

    [SerializeField]
    private LayerMask groundLayer; // 바닥 체크를 위한 충돌 레이어
    private CapsuleCollider2D capuleCollider2D; // 오브젝트의 충돌 범위 컴포넌트
    private bool isGrounded; // 바닥 체크 (바닥에 닿아있을 때 true)
    private Vector3 footPosition; // 발의 위치


    private void Start()
    {
        _player = GetComponent<Player>();

        _originPosition = transform.position;
    }

    private void Awake() // 플레이어 오브젝트가 만들어졌을 때
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        capuleCollider2D = GetComponent<CapsuleCollider2D>();
       
    }

    public void Jump()
    {
        if(isGrounded == true)
        { 
        _rigid2D.linearVelocity = Vector2.up * _bounce; // 위쪽방향 속력
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }
    public void Update()
    {
        float finalSpeed = _player.Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetBool("Run", true);
            finalSpeed = finalSpeed * ShiftSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * finalSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * finalSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        /*  if (Input.GetKey(KeyCode.S)) --시간되면 앉기 버튼 구현하기
            {
               transform.Translate(Vector3.left * Speed * Time.deltaTime);
             } */

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(h, v);
        direction.Normalize();

    }

    private void FixedUpdate()
    {
        Bounds bounds = capuleCollider2D.bounds; //플레이어 오브젝트의 위치정보
        footPosition = new Vector2(bounds.center.x, bounds.min.y); //플레이어 발 위치 설정
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer); //충돌이 일어나면isG 변수가 true가 될것이고 플레이어가 바닥을 밟고잇다.
    }

}
