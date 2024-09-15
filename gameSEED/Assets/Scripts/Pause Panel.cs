using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // For quitting to the main menu
using UnityEngine.UI; // For UI elements

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;  // Reference to the pause panel UI
    public Button resumeButton;    // Reference to the Resume button
    public Button quitButton;      // Reference to the Quit button
    private bool isPaused = false; // Track if the game is paused
    public GameObject optionsPanel;
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    void Start()
    {
        // Ensure the pause panel is hidden at the start
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
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

        if (filteredResolutions.Count == 0)
        {
            filteredResolutions.AddRange(resolutions);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex = 0;
        resolutionDropdown.RefreshShownValue();
        SetResolution(currentResolutionIndex);

        // Add listeners for the buttons
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitToMainMenu);
    }

    void Update()
    {
        // Check if Escape key is pressed to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }


    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    // Function to pause the game
    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Show the pause panel
        Time.timeScale = 0f; // Freeze the game
    }

    // Function to resume the game
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Hide the pause panel
        Time.timeScale = 1f; // Resume the game
    }

    // Function to quit to the main menu
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game isn't paused in the main menu
        SceneManager.LoadScene(0); // Load the MainMenu scene (using scene index -1)
    }
}
