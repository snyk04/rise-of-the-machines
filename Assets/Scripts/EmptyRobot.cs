using PlayerScripts;
using UnityEngine;

public class EmptyRobot : MonoBehaviour
{
    [SerializeField] PlayerChanger playerChanger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            playerChanger.ActivateEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            playerChanger.DeactivateEnter();
        }
    }
}
