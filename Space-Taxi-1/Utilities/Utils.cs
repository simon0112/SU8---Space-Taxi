using System;
using System.IO;
using System.Reflection;

namespace SpaceTaxi_1.Utilities {
    public class Utils {
        // makes sure that the path can be found on the computer.
       public static string GetLevelFilePath(string filename) {
            // Find base path
           DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location));

           while (dir.Name != "bin") {
                dir = dir.Parent;
           }

            dir = dir.Parent;

            string path = Path.Combine(dir.FullName.ToString(), "Levels", filename);

            //path = path.Replace("UnitTests\\UnitTests", "Space-Taxi-1");

            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }

            return path;
        }
    }
}
