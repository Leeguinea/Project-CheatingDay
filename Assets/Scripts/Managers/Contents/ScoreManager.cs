using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //싱글톤 패턴
    public static ScoreManager Instance { get; private set; }

    [SerializeField] 
    private int _score = 0;

    [SerializeField] 
    private TextMeshProUGUI _scoreText;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void ChangeScore(int amount)
    {
        _score += amount;
        UpdateScoreUI();

        //점수가 마이너스면 게임 오버 처리
        if (_score < 0)
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.EndGame(false);
            }
            else
            {
                Debug.LogError("씬에 GameManager가 없습니다.");
            }
        }
    }

    void UpdateScoreUI()
    {
        if (_scoreText != null)
            _scoreText.text = $"Score: {_score}";
    }

}
