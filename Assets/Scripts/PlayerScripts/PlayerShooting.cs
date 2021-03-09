using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GunController gun;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
    }
}
