using System.IO;

namespace Fdo.Contato.Vistoria.Droid.Extensions
{
    public static class PathExtensions
    {
        public static void DeleteDirectoryIfExists(this string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}