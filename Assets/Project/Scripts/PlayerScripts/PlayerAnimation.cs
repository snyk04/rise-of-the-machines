using Project.Classes.Damagable;
using UnityEngine;

namespace Project.Scripts.PlayerScripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int Strafe = Animator.StringToHash("Strafe");
        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Died = Animator.StringToHash("Died");

        public void Animate(float hor, float ver, bool isDead)
        {
            AnimateMove(hor, ver);
            AnimateDeath(isDead);
        }

        private void AnimateMove(float hor, float ver)
        {
            Player.Instance.Animator.SetBool(IsWalking, !hor.Equals(0) || !ver.Equals(0));

            Player.Instance.Animator.SetFloat(Strafe, hor);
            Player.Instance.Animator.SetFloat(Forward, ver);
        }
        private void AnimateDeath(bool isDead)
        {
            Player.Instance.Animator.SetBool(Died, isDead);
        }
    }
}
