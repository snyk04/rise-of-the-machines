using UnityEngine;

namespace PlayerScripts {
    public class PlayerAnimationController : MonoBehaviour {
        [SerializeField] private Animator humanAnimator;
        [SerializeField] private Animator robotAnimator;

        public void Animate(float hor, float ver, bool isDead) {
            AnimateMove(hor, ver);
            AnimateDeath(isDead);
            RobotAnimateMove(hor, ver);
            RobotAnimateDeath(isDead);
        }

        private void AnimateMove(float hor, float ver) {
            humanAnimator.SetBool("IsWalking", !hor.Equals(0) || !ver.Equals(0));

            humanAnimator.SetFloat("Strafe", hor);
            humanAnimator.SetFloat("Forward", ver);
        }

        private void AnimateDeath(bool isDead) {
            humanAnimator.SetBool("Died", isDead);
        }

        private void RobotAnimateMove(float hor, float ver)
        {
            robotAnimator.SetBool("IsWalking", !hor.Equals(0) || !ver.Equals(0));

            robotAnimator.SetFloat("Strafe", hor);
            robotAnimator.SetFloat("Forward", ver);
        }

        private void RobotAnimateDeath(bool isDead)
        {
            robotAnimator.SetBool("Died", isDead);
        }
    }
}