using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance = null;

    public int CurrentScore = 0;
    public static ScoreManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    public Animator ScoreAnimator;

    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _bestScoreTextUI;
    private int _currentScore = 0;
    private int _bestScore = 0;
    private Vector3 _originalScale;


    private const string CurrentScoreKey = "CurrentScore";
    private const string BestScoreKey = "BestScore";

    private void Start()
    {
        _originalScale = _currentScoreTextUI.transform.localScale;
        Load();
        _currentScore = 0;

        Refresh();
        Refreshbest();

    }

    public void AddScore(int score)
    {
        if (score < 0) return;
        _currentScore += score;

        AnimateScore();

        Refresh();

        UpdateBestScore();
        /*BossSpawner.instance.CheckSpawnBoss(_currentScore);  // 보스 체크 추가
*/
        Save();
    }

    public void BestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }

        Refreshbest();

        Savebest();
    }

    // 1. 하나의 메서드는 한가지 일만 잘 하면된다.

    private void Refresh()
    {
        _currentScoreTextUI.text = $"Score: {_currentScore:N0}";
    }

    private void Refreshbest()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore:N0}";
    }

    private void Save()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, _currentScore);
    }

    private void Savebest()
    {
        PlayerPrefs.SetInt(BestScoreKey, _bestScore);
    }

    private void Load()
    {
        _currentScore = PlayerPrefs.GetInt(CurrentScoreKey, 0);
        _bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    public void AnimateScore()
    {

        // 1.5배로 커졌다가 0.5초만에 원래 크기로 돌아오기
        _currentScoreTextUI.transform.DOScale(_originalScale * 1.5f, 0.25f)
            .OnComplete(() =>
                _currentScoreTextUI.transform.DOScale(_originalScale, 0.25f)
            );
    }

    // 최고 점수 갱신 함수 정의
    private void UpdateBestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            _bestScoreTextUI.text = $"BestScore: {_bestScore:N0}";

            // PlayerPrefs에 저장
            PlayerPrefs.SetInt(BestScoreKey, _bestScore);
            PlayerPrefs.Save();
        }
    }
}
