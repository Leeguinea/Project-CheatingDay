using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Common (Title & Play)")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioClip _buttonClickClip;

    [Header("Play Scene Only")]
    [SerializeField] private GameObject _pasuePanel;

    [Header("Title Scene Only")]
    [SerializeField] private GameObject _optionPanel;

   private bool _isPaused = false;


    void Awake() => Instance = this;

    void Start()
    {
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat("BGMVolume", -10f);
            _audioMixer.SetFloat("SFXVolume", -10f);
        }
    }

    void Update()
    {
        if(_pasuePanel != null && Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (_isPaused) 
                Resume();
            else 
                Pause();
        }
    }

    #region [Title & Play]
    
    //버튼 클릭음
    public void PlayClickSound()
    {
        if (SoundManager.Instance != null && _buttonClickClip)
            SoundManager.Instance.PlaySFX(_buttonClickClip);
    }

    //게임 종료
    public void OnClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); 
        #endif
    }

    //볼륨 조절
    public void SetBGMVolume(float volume) => _audioMixer.SetFloat("BGMVolume", volume); //배경음악
    public void SetSFXVolume(float volume) => _audioMixer.SetFloat("SFXVolume", volume); //효과음

    public void LoadGameScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    //옵션 창 열기
    public void OpenOption()
    {
        if (_optionPanel != null) _optionPanel.SetActive(true);
    }

    //옵션 창 닫기
    public void CloseOption()
    {
        if (_optionPanel != null) _optionPanel.SetActive(false);
    }

    #endregion


    #region [Play Scene Functions]

    //일시정지
    public void Pause()
    {
        SoundManager.Instance.PlaySFX(_buttonClickClip);
        _isPaused = true;
        if(_pasuePanel != null) _pasuePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    //재개
    public void Resume()
    {
        _isPaused = false;
        if(_pasuePanel != null) _pasuePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    //게임 종료
    public void ExitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("게임 종료");
        Application.Quit();
    }    

    //게임 재시작
    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        Debug.Log("Game Restart");
    }
    #endregion
}

