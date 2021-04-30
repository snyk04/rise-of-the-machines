using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMenu : MonoBehaviour
{
    public void PlayButton()
    {
        // SceneManager.LoadScene(1);
        StartCoroutine(ShowAndCloseLogo());
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public IEnumerator ShowAndCloseLogo()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            Debug.Log(asyncOperation.progress);
            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.25f);
                // asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
