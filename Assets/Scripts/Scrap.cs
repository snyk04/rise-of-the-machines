using PlayerScripts;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    private PlayerScrapController scrapInventoryController;

    private void Awake()
    {
        scrapInventoryController = PlayerController.playerController.GetComponent<PlayerScrapController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        scrapInventoryController.AmountOfScrap += 1;
    }
}
