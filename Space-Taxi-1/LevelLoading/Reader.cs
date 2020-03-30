using System.IO;
using System.Collections.Generic;
using SpaceTaxi.Utilities;

namespace SpaceTaxi.LevelLoading {
    public class Reader {
        public List<string> MapData {get; private set;}
        public List<string> MetaData {get; private set;}
        public List<string> LegendData {get; private set;}
        public List<string> CustomerData {get; private set;}

        public void ReadFile(string filename) {
            // Get each line of file as an entry in array lines.
            string[] lines = File.ReadAllLines(Utils.GetLevelFilePath(filename));

            // Iterate over lines and add data till the corresponding field
        }
    }
}
