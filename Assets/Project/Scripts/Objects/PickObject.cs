using Project.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Objects {
    [RequireComponent(typeof(Collider))]
    public abstract class PickObject : MonoBehaviour {
        public UnityAction ReadyToPickUp;
        public UnityAction NotReadyToPickUp;
        protected ICanBePickedUp item;
        private bool readyToPickUp;

        public bool TryPickObject() {
            if (readyToPickUp && item.TryPick()) {
                Destroy(gameObject);
                return true;
            }

            return false;
        }

        private void OnTriggerStay(Collider other) {
            if (other.TryGetComponent<CharacterController>(out _)) {
                ReadyToPickUp?.Invoke();
                readyToPickUp = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.TryGetComponent<CharacterController>(out _)) {
                NotReadyToPickUp?.Invoke();
                readyToPickUp = false;
            }
        }
    }
}