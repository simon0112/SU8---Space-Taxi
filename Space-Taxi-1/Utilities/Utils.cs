using System;
using System.IO;
using System.Reflection;

namespace SpaceTaxi_1.Utilities {
    public class Utils {
        // makes sure that the path can be found on the computer.

        //<summary> finds base path. And makes sure the the path is 
        //accesseble on the computer <summary/>
        //<input> type = string, name = filename </insput>
        //<return> static string </insput> 
       public static string GetLevelFilePath(string filename) {

            DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin")
            {
                dir = dir.Parent;
            }

            dir = dir.Parent;

            string path = Path.Combine(dir.FullName.ToString(), "Levels", filename);

             //path = path.Replace("UnitTests\\UnitTests", "Space-Taxi-1");

            if (!File.Exists(path)) {
                 throw new FileNotFoundException($"Error: The file \"{path}\" does not exist.");
            }

            return path;
            //return filename;
        }
    }
}
