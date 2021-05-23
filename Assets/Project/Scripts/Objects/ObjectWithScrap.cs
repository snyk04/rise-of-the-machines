using UnityEngine;

namespace Project.Scripts.Objects
{
    public class ObjectWithScrap : MonoBehaviour
    {
        public void Break()
        {
            Destroy(gameObject);
        }
    }
}
