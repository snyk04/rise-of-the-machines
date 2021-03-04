using UnityEngine;

namespace Player {
    public class PlayerAnimationController : MonoBehaviour {
        [SerializeField] private Animator animator;
        
        public void Animate(float hor, float ver, bool isDead) {
            AnimateMove(hor, ver);
            AnimateDeath(isDead);
        }

        private void AnimateMove(float hor, float ver) {
            animator.SetBool("IsWalking", !hor.Equals(0) || !ver.Equals(0));

            animator.SetFloat("Strafe", hor);
            animator.SetFloat("Forward", ver);
        }

        private void AnimateDeath(bool isDead) {
            animator.SetBool("Died", isDead);
        }
    }
}