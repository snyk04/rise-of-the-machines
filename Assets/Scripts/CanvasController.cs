using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    #region Properties

    [Header("UI elements")]
    [SerializeField] private GameObject enterText;
    [SerializeField] private GameObject exitText;
    [SerializeField] private Image cover;
    [SerializeField] private Text changeStateText;

    [Header("Settings")]
    [SerializeField] private float fadeSensitivity;
    [SerializeField] private float animationDelay;

    public GameObject EnterText { get => enterText; }
    public GameObject ExitText { get => exitText; }
    public Image Cover { get => cover; }
    public Text ChangeStateText { get => changeStateText; }

    private Coroutine animationCoroutine;

    #endregion

    public IEnumerator FadeScreen()
    {
        float transparency = 0;
        while (transparency < 1)
        {
            transparency += 0.01f * fadeSensitivity;
            cover.color = new Color(0, 0, 0, transparency);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator UnfadeScreen()
    {
        float transparency = 1;
        while (transparency > 0)
        {
            transparency -= 0.01f * fadeSensitivity;
            cover.color = new Color(0, 0, 0, transparency);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartAnimation(string[] textQuery)
    {
        animationCoroutine = StartCoroutine(AnimateChangeStateText(textQuery));
    }
    public void StopAnimation()
    {
        StopCoroutine(animationCoroutine);
        changeStateText.text = "";
    }
    private IEnumerator AnimateChangeStateText(string[] textQuery)
    {
        int state = 0;
        while (true)
        {
            changeStateText.text = textQuery[state % textQuery.Length];
            state += 1;
            yield return new WaitForSeconds(animationDelay);
        }
    }
}
