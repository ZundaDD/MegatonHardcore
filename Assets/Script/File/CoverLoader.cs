using System.IO;
using UnityEngine;
using UnityEngine.Splines;

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
            byte[] data = File.ReadAllBytes(CoverPath);
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, data);
            texture.wrapMode = TextureWrapMode.Clamp;
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
        }
    }
}