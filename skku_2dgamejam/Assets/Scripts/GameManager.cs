using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
 public static GameManager instance;

    public TextMeshProUGUI TimeTxt;
    public GameObject EndTxt;
    public GameObject Player;
    float time = 30.00f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {

            time -= Time.deltaTime;
            TimeTxt.text = time.ToString("N2"); //소숫점 둘째자리까지
            if (time < 00.00f)
            {
                GameOver(); // Text 형으로 안받고 GameObject형으로 받아서 생략가능
            }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;  // 게임 멈춤
        EndTxt.SetActive(true);  // 게임오버 텍스트 표시
    }

}
