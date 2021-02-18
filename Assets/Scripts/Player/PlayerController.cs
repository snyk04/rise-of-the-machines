using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController playerController;

        public Transform raycastTarget;

        void Awake()
        {
            playerController = this;
        }
    }
}
