﻿using System;
using Classes.TryInHierarchie;
using UnityEngine;
using UnityEngine.Events;

namespace Objects {
    [RequireComponent(typeof(Collider))]
    public abstract class PickObject : MonoBehaviour {
        public UnityAction ReadyToPickUp;
        public UnityAction NotReadyToPickUp;
        protected ICanBePickedUp item;
        private bool readyToPickUp;

        public bool TryPickObject() {
            if (readyToPickUp) {
                return item.TryPick();
            }

            return false;
        }

        private void OnTriggerEnter(Collider other) {

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