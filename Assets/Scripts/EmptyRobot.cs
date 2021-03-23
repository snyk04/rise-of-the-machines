using PlayerScripts;
using UnityEngine;

public class EmptyRobot : MonoBehaviour
{
    [SerializeField] PlayerChanger playerChanger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
        {
            playerChanger.ActivateEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
        {
            playerChanger.DeactivateEnter();
        }
    }
}
