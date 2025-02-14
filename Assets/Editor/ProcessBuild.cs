using System.IO;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class ProcessBuild
{
    [PostProcessBuild]
    public static void CopyDataFolder(BuildTarget target, string path)
    {
        string sourcesPath = Path.Combine(Application.dataPath, "../", "Data");
        string targetPath = Path.Combine(Path.GetDirectoryName(path), "Data");
        FileUtil.DeleteFileOrDirectory(targetPath);
        FileUtil.CopyFileOrDirectory(sourcesPath, targetPath);
    }
}
