using UnityEngine;

namespace Megaton.UI
{
    public class RotateImage : MonoBehaviour
    {
        [SerializeField] private float step = -0.1f;

        void Update()
        {
            transform.Rotate(new Vector3(0, 0, step));
        }
    }
}
