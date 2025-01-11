using UnityEngine;

namespace Megaton
{
    public class PlayController : MonoBehaviour
    {
        ProcessInput input;
        RailCollection rails;
        Chart recording;

        void Start()
        {
            input = new ProcessInput();
            rails = GetComponent<RailCollection>();
            recording = GameVar.Ins.CurPlay;
        }

    }
}