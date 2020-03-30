using System;
using System.IO;
using System.Reflection;

namespace SpaceTaxi.Utilities {
    public class Utils {
       public static string GetLevelFilePath(string filename) {
            // Find base path.
           DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location));

           while (dir.Name != "bin") {
                dir = dir.Parent;
           }

            dir = dir.Parent;

            string path = Path.Combine(dir.FullName.ToString(), "Levels", filename);

            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }

            return path;
        }
    }
}
