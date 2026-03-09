using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool _isPaused = false;

    [SerializeField]
    private GameObject _pasuePanel;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private AudioClip _buttonClickClip;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }
    }

    //일시정지
    public void Pause()
    {
        SoundManager.Instance.PlaySFX(_buttonClickClip);
        _isPaused = true;
        _pasuePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    //재개
    public void Resume()
    {
        _isPaused = false;
        _pasuePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    //게임 종료
    public void ExitGame()
    {
        Time.timeScale = 1f;
        //
        Debug.Log("게임 종료");
        Application.Quit();
    }

    //배경음악 볼륨 조절
    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat("BGMVolume", volume);
    }

    //효과음 볼륨 조절
    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFXVolume", volume);
    }

    //게임 재시작
    public void OnClickRestart()
    {
        Time.timeScale = 1f;

        //현재 활성화된 씬의 이름을 가져와 로드
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        Debug.Log("Game Restart");
    }
    
    //게임 종료
    public void OnClickExit()
    {
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 종료
        #else
                Application.Quit(); // 빌드된 게임에서 종료
        #endif

    }
}

