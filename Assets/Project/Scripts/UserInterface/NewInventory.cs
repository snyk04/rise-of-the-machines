using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UserInterface {
    public class NewInventory : MonoBehaviour
    {
        [SerializeField] private GameObject armorInventory;
        [SerializeField] private GameObject weaponsInventory;
        [SerializeField] private Button armorButton;
        [SerializeField] private Button weaponsButton;

        private void Start()
        {
            OpenArmorInventory();
        }

        public void OpenArmorInventory()
        {
            armorButton.interactable = false;
            weaponsButton.interactable = true;
            armorInventory.SetActive(true);
            weaponsInventory.SetActive(false);
        }

        public void OpenWeaponsInventory()
        {
            armorButton.interactable = true;
            weaponsButton.interactable = false;
            armorInventory.SetActive(false);
            weaponsInventory.SetActive(true);
        }
    }
}
