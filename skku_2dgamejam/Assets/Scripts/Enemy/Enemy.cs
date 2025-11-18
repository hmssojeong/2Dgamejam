using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    public float Speed = 3.0f;
    public float _health;
    public float Damage = 10f;

    [Header("적 타입")]
    public EEnemyType Type;

    [Header("점수")]
    public int Score;

    [Header("아이템 프리팹")]
    public GameObject[] ItemPrefabs;

    private Animator _animator;
    private Enemy _enemy;

    [Header("폭발 프리팹")]
    public GameObject ExplosionPrefab;


    [Header("사운드")]
    public AudioClip HitSound_Directional;
    public AudioClip HitSound_Trace;
    public AudioClip HitSound_Fly;
    public AudioClip DieSound_Trace;

    private AudioSource _audio;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();

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
        float moveFlyX = Speed * Time.deltaTime * -1f;

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

        transform.position = new Vector3(transform.position.x + moveFlyX, _currentPosition, 0);
    }

    private void DropItem()
    {
        if ((UnityEngine.Random.Range(0, 2) == 0)) return;
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터는 플레이어와만 충돌처리할 것이다.
        if (!other.gameObject.CompareTag("Player")) return;

        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        { 
        player.Hit(Damage);
        }
    }

    public void Hit(float Damage)
    {
        _health -= Damage;

        PlayHitSound();

        _animator.SetTrigger("Hit");
        if (ShakeCamera.Instance != null)
        {
            ShakeCamera.Instance.OnShakeCamera(0.2f, 0.2f);
        }

        if (_health <= 0 )
        {
            PlayDieSound();    
            StartCoroutine(Die(0.5f));
        }
    }

    private IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        Death();
    }

    private void PlayHitSound()
    {
        if (_audio == null) return;

        switch (Type)
        {
            case EEnemyType.Directional:
                if (HitSound_Directional != null)
                    _audio.PlayOneShot(HitSound_Directional);
                break;

            case EEnemyType.Trace:
                if (HitSound_Trace != null)
                    _audio.PlayOneShot(HitSound_Trace);
                break;

            case EEnemyType.Fly:
                if (HitSound_Fly != null)
                    _audio.PlayOneShot(HitSound_Fly);
                break;
        }
    }

    private void PlayDieSound()
    {
        if (_audio != null) return;
        
        switch (Type)
        {
            case EEnemyType.Trace:
                if (DieSound_Trace != null)
                    _audio.PlayOneShot(DieSound_Trace);
                break;
        }
    }

    private void Death()
    {

        DropItem();
        MakeExplosionEffect();
        _animator.SetTrigger("Die");
        Destroy(gameObject, 0.7f);
        ScoreManager.Instance.AddScore(Score);


    }

}