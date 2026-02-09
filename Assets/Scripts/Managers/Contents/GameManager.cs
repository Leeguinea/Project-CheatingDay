using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; //재시작 기능을 위함.

/*
 * [역할]
 * 1. 게임 시간(60초) 관리 및 UI 업데이트
 * 2. 승리(시간 종료) 및 패배(점수 미달) 상태 제어
 * 3. 게임 일시 정지 및 종료 처리
 */


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
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
                WinGame();
            }
        }

    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Time:" + timeRemaining.ToString("F1"); 
    }

    void WinGame()
    {
        isGameActive = false;
        timeRemaining = 0;
        if (timerText != null)
            timerText.text = "Victory!";

        //승리에도 게임 멈춤.
        Time.timeScale = 0f;

    }

    public void EndGame(bool isWin)
    {
        isGameActive = false;

        if (isWin)
        {
            WinGame();
        }
        else
        {
            if (timerText != null)
                timerText.text = "Game Over!";

            // 패배 시 게임 물리적 정지
            Time.timeScale = 0f;
        }
    }

}
