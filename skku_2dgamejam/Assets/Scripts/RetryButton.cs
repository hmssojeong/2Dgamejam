using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
   // 버튼이 클릭될 때 Retry() 함수 실행
   public void Retry()
    {
        // 'MainScene'이라는 씬 로드
        SceneManager.LoadScene("SampleScene");
    }
}
