using UnityEngine;

namespace PlayerScripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator humanAnimator;
        [SerializeField] private Animator robotAnimator;

        public void Animate(float hor, float ver, bool isDead)
        {
            AnimateHumanMove(hor, ver);
            AnimateHumanDeath(isDead);
            AnimateRobotMove(hor, ver);
            AnimateRobotDeath(isDead);
        }

        private void AnimateHumanMove(float hor, float ver)
        {
            humanAnimator.SetBool("IsWalking", !hor.Equals(0) || !ver.Equals(0));

            humanAnimator.SetFloat("Strafe", hor);
            humanAnimator.SetFloat("Forward", ver);
        }
        private void AnimateHumanDeath(bool isDead)
        {
            humanAnimator.SetBool("Died", isDead);
        }

        private void AnimateRobotMove(float hor, float ver)
        {
            robotAnimator.SetBool("IsWalking", !hor.Equals(0) || !ver.Equals(0));

            robotAnimator.SetFloat("Strafe", hor);
            robotAnimator.SetFloat("Forward", ver);
        }
        private void AnimateRobotDeath(bool isDead)
        {
            robotAnimator.SetBool("Died", isDead);
        }
    }
}
