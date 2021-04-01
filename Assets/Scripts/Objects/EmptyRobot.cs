using PlayerScripts;
using UnityEngine;

namespace Objects
{
    public class EmptyRobot : MonoBehaviour
    {
        [SerializeField] PlayerChanger playerChanger;

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
