﻿using Project.Classes.Characteristics;
using Project.Classes.ItemsAndInventory;
using UnityEngine;

namespace Project.Classes.Damagable {
    public class Player {
        public class UnityData {
            public Transform Transform { get; private set; }
            public Animator Animator { get; private set; }
            public CharacterController CharacterController { get; private set; }
            public Transform GunTransform { get; private set; }
            
            public bool Initialized { get; private set; }

            public void Initialize(Transform transform, Animator animator, CharacterController characterController,
                Transform gunTransform) {
                Transform = transform;
                Animator = animator;
                CharacterController = characterController;
                GunTransform = gunTransform;
                Initialized = true;
            }

            public void Uninitialize() {
                Transform = null;
                Animator = null;
                CharacterController = null;
                GunTransform = null;
                Initialized = false;
            }
        }

        public enum State {
            Human,
            Robot
        }

        public static Player Instance { get; private set; }

        public delegate void TakeDamage(float damage);

        public delegate void RestoreHealth(float heal);

        public delegate void StateChange();

        public TakeDamage takeDamage { get; private set; }
        public RestoreHealth restoreHealth { get; private set; }

        public StateChange stateChangedEvent;

        public HealthCharacteristic Health { get; private set; }
        public SpeedCharacteristic MoveSpeed { get; private set; }
        public ArmorCharacteristic Armor { get; private set; }

        public UnityData UnityPlayerData { get; private set; }

        public Inventory Inventory { get; } = new Inventory();

        private State currentState;
        private Human human;
        private Robot robot;

        public Human Human {
            get => human;
            private set { human = value; }
        }

        public Robot Robot {
            get => robot;
            private set { robot = value; }
        }

        public State CurrentState {
            get => currentState;
            set {
                if (currentState == value) return;
                currentState = value;
                OnChangeState();
                stateChangedEvent?.Invoke();
            }
        }

        public Player(Human human, Robot robot) {
            Human = human;
            Robot = robot;
            // UnityPlayerData = new UnityData();
            TransformIntoHuman();
            Instance = this;
        }
        
        public Transform Transform => UnityPlayerData.Transform;
        public Animator Animator => UnityPlayerData.Animator;
        public CharacterController CharacterController => UnityPlayerData.CharacterController;
        public Transform GunTransform => UnityPlayerData.GunTransform;

        private void OnChangeState() {
            if (currentState == State.Human) {
                TransformIntoHuman();
            }
            else {
                TransformIntoRobot();
            }
        }

        private void TransformIntoHuman() {
            TransformIntoObject(Human.TakeDamage, Human.RestoreHealth, Human.Health, Human.MoveSpeed, Human.PersonArmor,
                Human.UnityHumanData);
        }

        private void TransformIntoRobot() {
            TransformIntoObject(Robot.TakeDamage, Robot.RestoreHealth, Robot.Health, Robot.MoveSpeed, Robot.PersonArmor,
                Robot.UnityRobotData);
        }

        private void TransformIntoObject(TakeDamage objTakeDamage, RestoreHealth objRestoreHealth,
            HealthCharacteristic objHealth, SpeedCharacteristic objMoveSpeed, ArmorCharacteristic objArmor,
            UnityData objUnityData) {
            takeDamage = objTakeDamage;
            restoreHealth = objRestoreHealth;
            Health = objHealth;
            MoveSpeed = objMoveSpeed;
            Armor = objArmor;
            UnityPlayerData = objUnityData;
        }
    }
}