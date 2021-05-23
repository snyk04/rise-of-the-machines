using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UserInterface
{
    public class PopUpMessage : MonoBehaviour
    {
        [SerializeField] private Text message;
        [SerializeField] private GameObject messageObject;

        public void ShowMessage(string text, float timeToDismiss)
        {
            message.text = text;
            StartCoroutine(ShowingMessage(timeToDismiss));
        }

        private IEnumerator ShowingMessage(float timeToDismiss)
        {
            messageObject.SetActive(true);
            yield return new WaitForSeconds(timeToDismiss);
            messageObject.SetActive(false);
        }
    }
}
