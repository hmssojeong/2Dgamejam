using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeIntensity;
    private Coroutine shakeCoroutine;

    private Vector3 originalPosition;


    private void Awake()
    {
        instance = this;
        originalPosition = transform.position; // 게임 시작 시 위치 저장
    }

    public ShakeCamera()
     {
        // 자기 자신에 대한 정보를 static 형태의 instance 변수에 저장해서
        // 외부에서 쉽게 접근할 수 있도록 함.
         instance = this;
     }
    public void OnShakeCamera(float shakeTime=0.1f, float shakeIntensity=0.1f)
    {
        this.shakeTime = shakeTime; 
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");
    }

    private IEnumerator ShakeByPosition()
    {
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
            shakeTime -= Time.deltaTime;

            transform.position = originalPosition; // 반드시 원래 위치로 복귀
            yield return null;

        }

        transform.position = startPosition; //화면 흔들기 재생이 완료되면 스타트포지션위치로 다시 설정
        shakeCoroutine = null;
    }

    public void StopShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }
        transform.position = originalPosition; // 강제 복귀
    }
}
