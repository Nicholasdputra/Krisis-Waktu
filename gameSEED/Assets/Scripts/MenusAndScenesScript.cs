using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class MenusAndScenesScript : MonoBehaviour
{
    public GameObject mainMenuButtonsPanel;

    [Header("Options Panel")]
    public GameObject optionsPanel;
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    //Start Game
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Options
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuButtonsPanel.SetActive(false);
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
    

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuButtonsPanel.SetActive(true);
    }

    //Quit
    public void QuitGame()
    {
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        mainMenuButtonsPanel.SetActive(true);

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
}