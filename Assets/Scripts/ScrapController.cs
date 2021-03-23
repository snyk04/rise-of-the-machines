using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour
{
    [SerializeField] private SphereCollider scrapTrigger;
    [SerializeField] private GameObject scrapModel;
    [SerializeField] private GameObject player;
    [SerializeField] private ScrapInventoryController ScrapInventoryController;


    private void OnTriggerEnter(Collider player)
    {
        Destroy(scrapModel);
        ScrapInventoryController.scrapCounter += 1;
    }

}
