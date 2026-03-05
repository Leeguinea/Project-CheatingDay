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

    public void Pause()
    {
        SoundManager.Instance.PlaySFX(_buttonClickClip);
        _isPaused = true;
        _pasuePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _isPaused = false;
        _pasuePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        //
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFXVolume", volume);
    }

}
