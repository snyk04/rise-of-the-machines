using PlayerScripts;
using UnityEngine;

namespace Objects
{
    public class Scrap : MonoBehaviour
    {
        [SerializeField] private int amountOfScrap;

        private PlayerScrapController scrapInventoryController;

        private void Start()
        {
            scrapInventoryController = PlayerController.playerController.GetComponent<PlayerScrapController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
            scrapInventoryController.AmountOfScrap += amountOfScrap;
        }
    }
}
