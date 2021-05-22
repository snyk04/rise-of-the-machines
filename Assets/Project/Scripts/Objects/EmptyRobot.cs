using Project.Scripts.PlayerScripts;
using UnityEngine;

namespace Project.Scripts.Objects
{
    public class EmptyRobot : MonoBehaviour
    {
        [SerializeField] private PlayerChanger playerChanger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterController>(out _))
            {
                playerChanger.ActivateEnterText();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CharacterController>(out _))
            {
                playerChanger.DeactivateEnterText();
            }
        }
    }
}
