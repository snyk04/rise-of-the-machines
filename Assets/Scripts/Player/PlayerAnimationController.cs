using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Animate(float hor, float ver, bool isDead)
        {
            AnimateMove(hor, ver);
            AnimateDeath(isDead);
        }
        private void AnimateMove(float hor, float ver)
        {
            Vector2 rightVector = RightVector(hor, ver);
            animator.SetBool("IsWalking", !hor.Equals(0) || !ver.Equals(0));

            animator.SetFloat("Strafe", rightVector.x);
            animator.SetFloat("Forward", rightVector.y);
        }
        private void AnimateDeath(bool isDead)
        {
            animator.SetBool("Died", isDead);
        }

        private Vector2 RightVector(float hor, float ver)
        {
            Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
            Vector2 direction = new Vector2(hor, ver);

            float resultX;
            float resultY;
            if ((forward.x * hor < 0) || (forward.y * ver < 0))
            {
                resultY = -1;
            }
            else if (direction.Equals(Vector2.zero))
            {
                resultY = 0;
            }
            else
            {
                resultY = 1;
            }

            // if ((0.9f <= forward.x && forward.x <= 1) || (-1 <= forward.x && forward.x <= -0.9f))
            // {
            //     resultX = 0;
            // }
            if ((0 <= forward.y && forward.y <= 0.75f) || (-0.75f <= forward.y && forward.y <= 0))
            {
                resultX = ver;
            }
            else 
            {
                resultX = hor;
            }

            Debug.Log(resultX);
            // Debug.Log(forward + " " + direction + " " + result);
            return new Vector2(resultX, resultY);
        }
    }
}
