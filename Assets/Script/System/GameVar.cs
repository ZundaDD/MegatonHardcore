using System.IO;
using UnityEngine;

namespace Megaton
{
    public class GameVar
    {
        static GameVar ins = new();
        public static GameVar Ins => ins;

        public PlayMode PlayMode;
    }
}