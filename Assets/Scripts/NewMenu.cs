using InputHandling;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMenu : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject clickToLoadText;
    [SerializeField] private GameObject cover;
    
    private AsyncOperation asyncLoad;

    public void PlayButton()
    {
        StartCoroutine(LoadScene());
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        cover.SetActive(true);
        loadingText.SetActive(true);
        asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.SetActive(false);
                clickToLoadText.SetActive(true);
                InputMainMenu.Instance.mainMenuActions.Clicked.performed += context => AllowLoad();
                yield break;
            }
            yield return null;
        }
    }

    private void AllowLoad()
    {
        asyncLoad.allowSceneActivation = true;
    }
}
