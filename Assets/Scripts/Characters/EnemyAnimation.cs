using System;
using Classes;
using UnityEngine;

namespace Characters
{
    public class EnemyAnimation : MonoBehaviour
    {
        public enum State
        {
            Fighting,
            Idle,
            Moving
        }

        private static readonly int IsFighting = Animator.StringToHash("IsFighting");
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        
        private Enemy enemy;

        private void Start()
        {
            enemy = GetComponent<EnemyController>().Enemy;
        }

        public void SetAnimation(State state)
        {
            enemy.Animator.SetBool(IsFighting, false);
            enemy.Animator.SetBool(IsIdle, false);
            enemy.Animator.SetBool(IsMoving, false);
            switch (state)
            {
                case State.Fighting:
                    enemy.Animator.SetBool(IsFighting, true);
                    break;
                case State.Idle:
                    enemy.Animator.SetBool(IsIdle, true);
                    break;
                case State.Moving:
                    enemy.Animator.SetBool(IsMoving, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
