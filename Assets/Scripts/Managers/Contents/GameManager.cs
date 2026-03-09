using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; //재시작 기능을 위함.
using UnityEngine.UI;
using System.Collections;

/*
 * [역할]
 * 1. 게임 시간(60초) 관리 및 UI 업데이트
 * 2. 승리(시간 종료) 및 패배(점수 미달) 상태 제어
 * 3. 시작 전 UI 비활성화 및 카운트다운 후 활성화 처리
 */

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public GameObject resultPanel;
    public TextMeshProUGUI penaltyText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI countdownText;

    [Header("Game References")]
    public float timeRemaining = 60f;
    private bool isGameActive = true;

    void Start()
    {
        StartCoroutine(StartGameRoutine());
    }

    void Update()
    {
        if(isGameActive)
        {
            if(timeRemaining > 0) 
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else 
            {
                EndGame(true);
            }
        }
    }

    //게임 시작(카운트 다운)
    IEnumerator StartGameRoutine()
    {
        Time.timeScale = 0;
        isGameActive = false;
        countdownText.gameObject.SetActive(true);

        if (timerText != null)
            timerText.gameObject.SetActive(false);
        if (penaltyText != null)
            penaltyText.gameObject.SetActive(false);
        ScoreManager.Instance.SetScoreUIActive(false);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text = "START!";
        yield return new WaitForSecondsRealtime(0.5f);

        if (timerText != null) 
            timerText.gameObject.SetActive(true);
        if (penaltyText != null)
            penaltyText.gameObject.SetActive(true);
        ScoreManager.Instance.SetScoreUIActive(true);

        countdownText.gameObject.SetActive(false);
        isGameActive = true;
        Time.timeScale = 1;
    }

    //??
    void UpdateTimerUI()
    {
        if(timerText != null)
            timerText.text = "Time:" + timeRemaining.ToString("F1"); 
    }

    //게임 종료
    public void EndGame(bool isWin)
    {
        //이미 종료되었으면 리턴
        if(!isGameActive)
            return;

        isGameActive = false;
        Time.timeScale = 0f;

        //타이머 텍스트 변경
        if(timerText != null)
            timerText.text = isWin ? "Victory" : "Game Over!";

        //결과창 비활성화
        if(resultPanel != null)
            resultPanel.SetActive(true);

        //점수 처리(ScoreManager와 연동)
        int currentScore = ScoreManager.Instance.GetCurrentScore();
        ScoreManager.Instance.UpdateBestScore(currentScore);

        //결과 UI 텍스트 업데이트
        if(finalScoreText != null)
            finalScoreText.text = $"Final Score: {currentScore}";

        if(bestScoreText != null)
            bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("BestScore", 0)}";
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
