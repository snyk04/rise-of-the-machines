using Project.Classes.Damagable;
using UnityEngine;

namespace Project.Scripts.Characters
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

        public void SetAnimation(State state)
        {
            enemy = GetComponent<EnemyController>().Enemy;
            
            enemy.Animator.SetBool(IsFighting, state == State.Fighting);
            enemy.Animator.SetBool(IsIdle, state == State.Idle);
            enemy.Animator.SetBool(IsMoving, state == State.Moving);
        }
    }
}
