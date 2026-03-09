using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; //재시작 기능을 위함.
using UnityEngine.UI;

/*
 * [역할]
 * 1. 게임 시간(60초) 관리 및 UI 업데이트
 * 2. 승리(시간 종료) 및 패배(점수 미달) 상태 제어
 * 3. 게임 일시 정지 및 종료 처리
 */

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public GameObject resultPanel; //결과 패널
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;

    [Header("Game References")]
    public float timeRemaining = 60f;
    private bool isGameActive = true;
    
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
