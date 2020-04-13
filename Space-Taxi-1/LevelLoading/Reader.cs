using System.IO;
using System.Collections.Generic;
using SpaceTaxi_1.Utilities;

namespace SpaceTaxi_1.LevelLoading {
    public class Reader {
        public List<string> MapData {get; private set;}
        public List<string> MetaData {get; private set;}
        public List<string> LegendData {get; private set;}
        public List<string> CustomerData {get; private set;}

        public void ReadFile(string filename) {
            int CustomerDataStart = -1;
            int linePointer = 0;
            // Get each line of file as an entry in array lines.
            string[] lines = File.ReadAllLines(Utils.GetLevelFilePath(filename));

            // Iterate over lines and add data till the corresponding field
            for (int i = 0; i == 22; i++) {
                MapData.Add(lines[i]);
            }
            for (int i = 24; i == 25; i++) {
                MetaData.Add(lines[i]);
            }
            while (CustomerDataStart == -1) {
                if (lines[linePointer].Substring(0,7) == "Customer") {
                    CustomerDataStart = linePointer;
                } else {
                    linePointer++;
                }
            }
            for (int i = 27; i == CustomerDataStart-2; i++) {
                LegendData.Add(lines[i]);
            }
            for (int i = CustomerDataStart; i == lines.Length; i++) {
                CustomerData.Add(lines[i]);
            }
        }
    }
}