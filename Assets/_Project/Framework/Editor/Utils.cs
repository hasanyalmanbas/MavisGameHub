using System.IO;

namespace _Project.Framework.Editor
{
    public static class Utils
    {
        public static void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void CreateFolderAndSubfolders(string rootPath, string[] subFolders)
        {
            foreach (var path in subFolders)
            {
                CreateFolder(Path.Combine(rootPath, path));
            }
        }
    }
}