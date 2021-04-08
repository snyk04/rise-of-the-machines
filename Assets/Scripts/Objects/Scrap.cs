using PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class Scrap : MonoBehaviour
    {
        [SerializeField] private int amountOfScrap;
        
        public UnityEvent pickedUp;

        private PlayerScrapController scrapInventoryController;

        private void Start()
        {
            scrapInventoryController = PlayerController.playerController.GetComponent<PlayerScrapController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            scrapInventoryController.AmountOfScrap += amountOfScrap;

            pickedUp.Invoke();
            Destroy(gameObject);
        }
    }
}
