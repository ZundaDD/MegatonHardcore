using UnityEngine;

namespace Megaton
{
    public class PlayController : MonoBehaviour
    {
        ProcessInput input;
        RailCollection rails;

        void Start()
        {
            input = new ProcessInput();
            rails = GetComponent<RailCollection>();
        }

    }
}