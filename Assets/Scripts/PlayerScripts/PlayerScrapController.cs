using UnityEngine;

namespace PlayerScripts
{
    public class PlayerScrapController : MonoBehaviour
    {
        public int AmountOfScrap { get; set; }

        public void Awake()
        {
            AmountOfScrap = 0;
        }
    }
}
