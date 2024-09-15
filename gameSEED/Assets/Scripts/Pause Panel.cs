using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For quitting to the main menu
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;  // Reference to the pause panel UI
    public Button resumeButton;    // Reference to the Resume button
    public Button quitButton;      // Reference to the Quit button
    private bool isPaused = false; // Track if the game is paused
    private bool isntInOptions = true; // Track if the player is in the options menu
    public GameObject optionsPanel;
    [Header("Options Panel")]
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    // New: Sliders for music and SFX
    public Slider musicSlider;
    public Slider sfxSlider;

    // Assume AudioSource references for BGM and SFX
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip clickClip;
    void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        // Initialize the sliders with saved values (or default 1.0)
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        // Attach listener functions to the sliders
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);

        // Set initial volume based on saved values
        bgmSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;

        // Add listeners for the buttons
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitToMainMenu);

        // Other existing initialization code...
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        if (filteredResolutions.Count == 0)
        {
            filteredResolutions.AddRange(resolutions);
        }

        // Sort resolutions by width and height
        filteredResolutions = filteredResolutions.OrderByDescending(res => res.width).ThenByDescending(res => res.height).ToList();


        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.value.ToString("0.##") + " Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height && (float)filteredResolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        if (filteredResolutions.Count > 0)
        {
            if (currentResolutionIndex < 0 || currentResolutionIndex >= filteredResolutions.Count)
            {
                currentResolutionIndex = 0; // Default to the first resolution if index is out of range
            }
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            SetResolution(currentResolutionIndex);
        }
        else
        {
            float currentRefreshRate;
            currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;
            Debug.Log("Current Refresh Rate: " + currentRefreshRate.ToString("0.##") + " Hz");
            Debug.LogError("No resolutions available for the current refresh rate.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && isntInOptions)  // Only allow pausing if not in the options menu
            {
                ResumeGame();
            }
            else if(!isPaused && isntInOptions)  // Only allow pausing if not in the options menu
            {
                PauseGame();
            }
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        if (filteredResolutions == null || filteredResolutions.Count == 0)
        {
            Debug.LogError("Filtered resolutions list is empty.");
            return;
        }

        if (resolutionIndex < 0 || resolutionIndex >= filteredResolutions.Count)
        {
            Debug.LogError($"Resolution index {resolutionIndex} is out of range. Valid range is 0 to {filteredResolutions.Count - 1}.");
            return;
        }

        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
    // Update BGM volume when slider changes
    private void OnMusicSliderValueChanged(float value)
    {
        bgmSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value); // Save the volume setting
    }

    // Update SFX volume when slider changes
    private void OnSFXSliderValueChanged(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value); // Save the volume setting
    }

    public void OpenOptions()
    {
        isntInOptions = false;
        sfxSource.PlayOneShot(clickClip);
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptions()
    {
        isntInOptions = true;
        sfxSource.PlayOneShot(clickClip);
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    void PauseGame()
    {
        sfxSource.PlayOneShot(clickClip);
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        sfxSource.PlayOneShot(clickClip);
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        sfxSource.PlayOneShot(clickClip);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
