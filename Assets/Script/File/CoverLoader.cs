using System.IO;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 封面加载器，默认400*400，png格式
    /// </summary>
    public static class CoverLoader
    {
        public static string CoverPath;
        public static string CoverName = "cover.png";

        public static Sprite Path2Sprite(string path)
        {
            CoverPath = Path.Combine (path, CoverName);
            return null;
        }
    }
}