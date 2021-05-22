using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts {
    public class MainMenu : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] private GameObject humanText;
        [SerializeField] private GameObject robotText;
        [SerializeField] private GameObject humanObject;
        [SerializeField] private GameObject robotObject;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject storageText;
        [SerializeField] private GameObject craftText;
        [SerializeField] private Dropdown graphicsDropdown;
        [SerializeField] private Dropdown resolutionDropdown;

        private void Start()
        {
            HumanButton();
        }

        public void PlayButton()
        {
            SceneManager.LoadScene(1);
        }
        public void ExitButton()
        {
            Application.Quit();
        }

        public void HumanButton()
        {
            humanObject.SetActive(true);
            humanText.SetActive(true);
            robotObject.SetActive(false);
            robotText.SetActive(false);
        }
        public void RobotButton()
        {
            humanObject.SetActive(false);
            humanText.SetActive(false);
            robotObject.SetActive(true);
            robotText.SetActive(true);
        }

        public void StorageButton()
        {
            storageText.SetActive(true);
            craftText.SetActive(false);
        }
        public void CraftButton()
        {
            storageText.SetActive(false);
            craftText.SetActive(true);
        }

        public void SettingsButton()
        {
            settingsPanel.SetActive(true);
        }
        public void SettingsButtonExit()
        {
            settingsPanel.SetActive(false);
        }

        public void GraphicsChanger()
        {
            switch (graphicsDropdown.value)
            {
                case 0:
                    QualitySettings.SetQualityLevel(1, true);
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(3, true);
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(6, true);
                    break;
            }
        }
        public void ResolutionChanger()
        {
            switch (resolutionDropdown.value)
            {
                case 0:
                    Screen.SetResolution(640, 480, true);
                    Debug.Log("loh");
                    break;
                case 1:
                    Screen.SetResolution(1366, 768, true);
                    break;
                case 2:
                    Screen.SetResolution(1920, 1080, true);
                    break;
            }
        }
    }
}
