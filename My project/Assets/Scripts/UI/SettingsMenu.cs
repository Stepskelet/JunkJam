using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] public GameObject mainMenu;
    [SerializeField]public GameObject pauseMenu;
    [SerializeField]public GameObject settingsMenu;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private TMP_Text textSFX;
    [SerializeField] private TMP_Text textMusic;

    float musicVolume = 1f;
    float SFXVolume = 1f;
    private bool musicMuted = false;
    private bool SFXMuted = false;



    private void Start()
    {
        audioMixer.SetFloat("MusicVolumeMixer", 0f);
        audioMixer.SetFloat("SFXVolumeMixer", 0f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettingsMenu();
        }
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        audioMixer.SetFloat("MusicVolumeMixer", Mathf.Log10(musicVolume)*20);
    }
    public void MuteMusic()
    {
        if(!musicMuted)
        {
            audioMixer.SetFloat("MusicVolumeMixer", -100f);
            textMusic.fontStyle = FontStyles.Strikethrough;
            musicMuted= true;
        }
        else
        {
            audioMixer.SetFloat("MusicVolumeMixer", Mathf.Log10(musicVolume) * 20);
            textMusic.fontStyle = FontStyles.Normal;
            musicMuted= false;
        }

    }

    public void MuteSFX()
    {
        if (!SFXMuted)
        {
            audioMixer.SetFloat("SFXVolumeMixer", -100f);
            textSFX.fontStyle = FontStyles.Strikethrough;
            SFXMuted= true;
        }
        else
        {
            audioMixer.SetFloat("SFXVolumeMixer", Mathf.Log10(SFXVolume) * 20);
            textSFX.fontStyle = FontStyles.Normal;
            SFXMuted = false;
        }
    }

    public void SetSFXVolume()
    {
        SFXVolume = SFXSlider.value;
        audioMixer.SetFloat("SFXVolumeMixer", Mathf.Log10(SFXVolume) * 20);
    }

    public void CloseSettingsMenu()
    {
        if (MainMenu.mainMenuActive)
        {
            mainMenu.SetActive(true);
        }
        if (PauseMenu.isPaused)
        {
            pauseMenu.SetActive(true);
        }
        settingsMenu.SetActive(false);
    }
}
