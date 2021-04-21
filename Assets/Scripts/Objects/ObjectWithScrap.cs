using UnityEngine;

namespace Objects
{
    public class ObjectWithScrap : MonoBehaviour
    {
        public void Break()
        {
            Destroy(gameObject);
        }
    }
}
