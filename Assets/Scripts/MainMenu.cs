using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject PersText;
    public GameObject RobotText;
    public GameObject PersModel;
    public GameObject RobotModel;
    public GameObject settingsPanel;
    public GameObject skladText;
    public GameObject craftText;
    public Dropdown graphDropdown;
    public Dropdown resolutionDropdown;

    void Start()
    {
        PersModel.SetActive(true);
        PersText.SetActive(true);
        RobotModel.SetActive(false);
        RobotText.SetActive(false);
    }

    public void exitButtnon()
    {
        Application.Quit();
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void persButton()
    {
        PersModel.SetActive(true);
        PersText.SetActive(true);
        RobotModel.SetActive(false);
        RobotText.SetActive(false);
    }
    public void robotButton()
    {
        PersModel.SetActive(false);
        PersText.SetActive(false);
        RobotModel.SetActive(true);
        RobotText.SetActive(true);
    }

    public void skladButton()
    {
        skladText.SetActive(true);
        craftText.SetActive(false);
    }
    public void craftButton()
    {
        skladText.SetActive(false);
        craftText.SetActive(true);
    }

    public void settingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void settingsButtonExit()
    {
        settingsPanel.SetActive(false);
    }

    public void graphicsChanger()
    {
        if (graphDropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        if (graphDropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(3, true);
        }
        if (graphDropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(6, true);
        }

    }

    public void resolutionChanger()
    {
        if (resolutionDropdown.value == 0)
        {
            Screen.SetResolution(640, 480, true);
            Debug.Log("loh");
        }
        if (resolutionDropdown.value == 1)
        {
            Screen.SetResolution(1366, 768, true);
        }
        if (resolutionDropdown.value == 2)
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }

    void Update()
    {
        
    }
}
