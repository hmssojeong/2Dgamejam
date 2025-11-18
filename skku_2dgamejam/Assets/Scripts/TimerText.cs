using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 30f;
    public float warningTime = 5f;

    private void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if(timeRemaining < warningTime && timeRemaining > 0)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = Color.white;
            }

            timerText.text = Mathf.Ceil(timeRemaining).ToString("F0"); //소숫점 첫번째 자리에서 올림
        }
    }
}
